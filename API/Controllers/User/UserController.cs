using API.Setup.Services;
using Application.Features.User;
using Domain.User;
using Domain.User.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using Humanizer;
using Application.Core.Logging;
using Infrastructure.Security;
using Application.Interfaces;
using API.Setup;
using System.Globalization;
using Application.Interfaces.Settings;

namespace API.Controllers.User
{
    // [ApiController]
    // [Route("api/[controller]")]

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ISettings _settings;
        private readonly IAuthenticationService _googleAuthenticationService;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService,
                RoleManager<IdentityRole> roleMgr, ISettings settings, IAuthenticationService googleAuthenticationService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleMgr;
            _settings = settings;
            _googleAuthenticationService = googleAuthenticationService;
        }

        #region USER 
        [HttpPut("user")]
        public async Task<ActionResult<UserDto>> DeleteMultiple(List<RegisterDto> registerDto)
        {
            foreach (RegisterDto regDto in registerDto)
            {
                var user = await _userManager.Users.Where(x => x.UserName == regDto.Username && x.Email == regDto.Email).FirstAsync();
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }

            return BadRequest("Problem deleting users");
        }

        [HttpPost("user")]
        public async Task<IActionResult> EditUser([FromBody] UserListDto dto)
        {
            var currentUser = await _userManager.Users.Where(x => x.Id == dto.Id).FirstAsync();
            if (await _userManager.Users.AnyAsync(x => x.UserName == dto.UserName))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            if (currentUser != null)
            {
                currentUser.Email = dto.Email;
                currentUser.DisplayName = dto.DisplayName;
                currentUser.UserName = dto.UserName;
                var result = await _userManager.UpdateAsync(currentUser);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest("Problem editing user");
        }

        [HttpGet("user")]
        public async Task<UserListDto> GetUser(string? Id)
        {

            List<UserListDto> users = new();

            var appUsers = await _userManager.Users
                                 .Where(x => x.Id == Id)
                                .Include(x => x.GroupUsers)
                                .ThenInclude(g => g.Group)
                                .ToListAsync();
            if (appUsers != null)
            {

                foreach (var usr in appUsers)
                {
                    UserListDto user = new();
                    List<Group> groups = new();
                    user.DisplayName = usr.DisplayName;
                    user.UserName = usr.UserName;
                    user.Email = usr.Email;
                    user.Id = usr.Id;
                    user.GroupsCount = usr.GroupUsers.Count;
                    user.Groups = groups;
                    users.Add(user);
                }
                return users[0];
            }

            return await Task.FromResult<UserListDto>(new());

        }

        [HttpGet()]
        public async Task<List<UserListDto>> GetallUsers()
        {
            List<UserListDto> users = new();
            var appUsers = await _userManager.Users
                         .Include(x => x.GroupUsers)
                          .ThenInclude(g => g.Group)
                          .ToListAsync();

            foreach (var usr in appUsers)
            {
                UserListDto user = new();
                List<Group> groups = new();
                user.DisplayName = usr.DisplayName;
                user.UserName = usr.UserName;
                user.Email = usr.Email;
                user.Id = usr.Id;
                user.OTPAuthentication = usr.OTPAuthentication;
                user.GoogleAuthenticator = usr.OTPKey !=null && usr.OTPKey.Length>4 ? true : false;
                user.GroupsCount = usr.GroupUsers.Count;
                user.Groups = groups;
                users.Add(user);
            }
            
            return users;
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
            else
            {
                Console.WriteLine("gk errors" + result.Errors.ToString());
            }


            return BadRequest("Problem adding user");
        }

        [HttpPost("reset-otpauth/{id}")]
        [Authorize]
        public async Task<IActionResult> ResetOtpAuth(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (user != null)
            {
               var otpkey= _googleAuthenticationService.GetOTPKey(id);
                if (user.OTPKey !=null || user?.OTPKey?.Length >3 )
                {
                    user.OTPKey = null;
                    user.OTPAuthentication = false;
                }
                else
                {
                    user.OTPKey = otpkey.Value;
                }

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
           
            }

            return Ok();
        }

        [HttpPost("user-group")]
        public async Task<IActionResult> addUserGroups([FromBody] List<UsersListDto> registerDto)
        {
            return HandleResult(await Mediator.Send(new AddUserGroup.Command(registerDto)));
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

        #endregion

    }
}