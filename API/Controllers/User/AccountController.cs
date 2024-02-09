using System;
using System.Globalization;
using API.Setup;
using API.Setup.Services;
using Application.Interfaces.Settings;
using Domain.User;
using API.Controllers.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using Application.Core.Logging;
using Elsa.Activities.Primitives;
using System.Net;
using Infrastructure.Security;
using Application.Interfaces;
using System.Text;


// Cross cutting concern, no repository since we only want authentication to access it
// Clean architecture exception
namespace API.Controllers.User
{
    [AllowAnonymous]
    // [ApiController]
    // [Route("api/v1.0/[controller]")]

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly ISettings _settings;
        private readonly IAuthenticationService _googleAuthenticationService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            TokenService tokenService, ISettings settings, IAuthenticationService googleAuthenticationService)
        {
            _settings = settings;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _googleAuthenticationService = googleAuthenticationService;
        }

        // TODO: Update cookie options to make them secure in production
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null) return Unauthorized();

            var otpAuthentication = user.OTPAuthentication;
            var googleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false;

            if (googleAuthenticator)
            {
                string qrCodeImage="";

                if (!otpAuthentication)
                {
                    var qrcd = _googleAuthenticationService.GenerateQrCode(user.Id, user.OTPKey !=null ? user.OTPKey  : "");
                    qrCodeImage = qrcd.Value !=null ? qrcd.Value  : "" ;
                }                          

                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    QrCodeImage = qrCodeImage,
                    Username = user.UserName,
                    MinutesRemaining = 0,
                    OtpAuthentication = user.OTPAuthentication,
                    GoogleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false,
                };

            }            
            else {

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                {
                var minutesRemaining = await _settings.GetSessionTimeoutAsync();
                var token = await _tokenService.CreateToken(user);
                var expires = DateTime.UtcNow.AddMinutes(minutesRemaining);

                var expirationCookieOptions = new CookieOptions()
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(minutesRemaining),
                    SameSite = SameSiteMode.None
                };
                var jwtCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(minutesRemaining),
                    SameSite = SameSiteMode.None
                };

                Response.Cookies.Append("EXPIRATION_TIME", expires.ToString("o"), expirationCookieOptions);
                Response.Cookies.Append("ACCESS_TOKEN_COOKIE", token, jwtCookieOptions);

                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    QrCodeImage = null,
                    Username = user.UserName,
                    MinutesRemaining = minutesRemaining,
                    OtpAuthentication = user.OTPAuthentication,
                    GoogleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false,
                };
            }
             
            }
            var resultt = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, true,  false);
            

            return Unauthorized();
        }

        [HttpPost("register-otpauth")]
        public async Task<ActionResult<UserDto>> RegisterOtpAuth(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return Unauthorized("No active user found");
            };

            var resultsUser = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (resultsUser.Succeeded)
            {
                //var otpkey = await _googleAuthenticationService.GetOTPKey(user.Id);
                if (user.OTPKey != null || user?.OTPKey?.Length > 3)
                {
                    user.OTPAuthentication = true;
                }

                if (user == null)
                {
                    return Unauthorized("No active user found");
                };

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Username = user.UserName,
                    OtpAuthentication = user.OTPAuthentication,
                    GoogleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false,
                };

            }

            return Unauthorized("No active user found");

        }

        [HttpGet("otp")]
        public async Task<ActionResult<string>?> GetCurrentUserOTPImage()
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }

            if (Request.Cookies.TryGetValue("EXPIRATION_TIME", out string? expirationTimeString))
            {
                DateTime expirationTime = DateTime.Parse(expirationTimeString!, null, DateTimeStyles.RoundtripKind);
                int minutesRemaining = (int)(expirationTime - DateTime.UtcNow).TotalMinutes;

                if (user.OTPKey?.Length > 3 && user.OTPKey != null)
                {
                    var qrcd =  _googleAuthenticationService.GenerateQrCode(user.Id, user.OTPKey);
                    return qrcd.Value !=null ? qrcd.Value :"";
                }
                return null;

            }

            return new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("No active session found"));

        }

        [HttpPost("otp/{code}")]
        public async Task<ActionResult<UserDto>> ValidateOtpCode(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return Unauthorized("No active user found");
            };

            var resultsUser = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (resultsUser.Succeeded && loginDto.OtpCode != null && loginDto.OtpCode != "" && user.OTPKey != null)
            {
                var validate = _googleAuthenticationService.ValidateOTP(loginDto.OtpCode, user.OTPKey);

                if (validate.IsSuccess)
                {
                    var minutesRemaining = await _settings.GetSessionTimeoutAsync();
                    var token = await _tokenService.CreateToken(user);
                    var expires = DateTime.UtcNow.AddMinutes(minutesRemaining);

                    var expirationCookieOptions = new CookieOptions()
                    {
                        HttpOnly = true,
                        IsEssential = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddMinutes(minutesRemaining),
                        SameSite = SameSiteMode.None
                    };
                    var jwtCookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        IsEssential = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddMinutes(minutesRemaining),
                        SameSite = SameSiteMode.None
                    };

                    Response.Cookies.Append("EXPIRATION_TIME", expires.ToString("o"), expirationCookieOptions);
                    Response.Cookies.Append("ACCESS_TOKEN_COOKIE", token, jwtCookieOptions);

                    return new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Image = null,
                        QrCodeImage = null,
                        Username = user.UserName,
                        MinutesRemaining = minutesRemaining,
                        OtpAuthentication = user.OTPAuthentication,
                        GoogleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false,
                    };

                    //return Ok("Success");

                }
            }

            return BadRequest("Problem Validating OTP");
        }

        [HttpGet("logout")]
        public IActionResult LogoutUser()
        {
            Console.WriteLine("logout ");

            var expires = DateTime.UtcNow.AddDays(-1);

            //AddMinutes(-60);

            Response.Cookies.Delete("ACCESS_TOKEN_COOKIE");
            Response.Cookies.Delete("EXPIRATION_TIME");

            Response.Cookies.Append("EXPIRATION_TIME", expires.ToString("o"));

            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Bio = "Placeholder"
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest("Problem adding user");
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await GetUser();
            if (user == null)
            {
                return Unauthorized("No active user found");
            };

            if (Request.Cookies.TryGetValue("EXPIRATION_TIME", out string? expirationTimeString))
            {
                DateTime expirationTime = DateTime.Parse(expirationTimeString!, null, DateTimeStyles.RoundtripKind);
                int minutesRemaining = (int)(expirationTime - DateTime.UtcNow).TotalMinutes;

                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Username = user.UserName,
                    MinutesRemaining = minutesRemaining,
                    OtpAuthentication = user.OTPAuthentication,
                    GoogleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false,
                };

            }

            return new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("No active session found"));
        }



        [Authorize]
        [HttpPost("bic")]
        public async Task<ActionResult<UserDto>> SetActiveATM(ActiveATMDto activeATMDto)
        {
            var user = await GetUser();
            if (user == null)
            {
                return Unauthorized("No active user found");
            };

            try
            {
                if (Request.Cookies.TryGetValue("EXPIRATION_TIME", out string? expirationTimeString))
                {
                    user.RegistrationBank = activeATMDto.ActiveATM;
                    await _userManager.UpdateAsync(user);

                    DateTime expirationTime = DateTime.Parse(expirationTimeString!, null, DateTimeStyles.RoundtripKind);
                    int minutesRemaining = (int)(expirationTime - DateTime.UtcNow).TotalMinutes;

                    return new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Image = null,
                        Username = user.UserName,
                        MinutesRemaining = minutesRemaining,
                        OtpAuthentication = user.OTPAuthentication,
                        GoogleAuthenticator = user.OTPKey != null && user.OTPKey.Length > 4 ? true : false,
                    };
                }
            }
            catch
            {
                return new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("Unable to effect active ATM change"));
            }
            return new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("No active session found"));
        }

        private async Task<AppUser?> GetUser()
        {

            var username = User.Identity?.Name;
            if (username == null)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        private async Task<AppUser?> RemoveUser()
        {
                  

            var username = User.Identity?.Name;
            if (username == null)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }
           
            await _signInManager.SignOutAsync();

            var cv = await _userManager.RemoveLoginAsync(user, "", "");
            var usernames = User.Identity?.Name;
            if (usernames == null)
            {
                return null;
            }

            return user;
        }

    }
}