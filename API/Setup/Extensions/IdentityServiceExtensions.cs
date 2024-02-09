using System.Text;
using System.Text.Json;
using API.Setup.Services;
using Application.Core.Exceptions;
using Domain.User;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
namespace API.Setup.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddClaimsPrincipalFactory<GroupClaimsPrincipalFactory>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();

            var tokenKey = config["TokenKey"];
            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new ArgumentNullException(nameof(tokenKey), "Encrypytion key not discovered");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)                               
                        .AddJwtBearer(opt =>
                        {
                            opt.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = key,
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                // Above become true as well
                                // ValidAudience = "Your Audience"
                                // ValidIssuer = "Your Issuer
                                ValidateLifetime = true,
                            };
                            opt.Events = new JwtBearerEvents
                            {
                                OnMessageReceived = context =>
                                {
                                    var accessToken = context.Request.Cookies["ACCESS_TOKEN_COOKIE"];
                                    var path = context.HttpContext.Request.Path;
                                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                                    {
                                        context.Token = accessToken;
                                    }
                                    else if (!string.IsNullOrEmpty(accessToken))
                                    {
                                        context.Token = accessToken;
                                    }
                                    return Task.CompletedTask;
                                },
                                OnChallenge = context =>
                                {
                                    context.HandleResponse();
                                    context.Response.StatusCode = 401;
                                    context.Response.ContentType = "application/json";

                                    // Always want an error 
                                    if (string.IsNullOrEmpty(context.Error))
                                    {
                                        context.Error = "invalid_token";
                                    }
                                    if (string.IsNullOrEmpty(context.ErrorDescription))
                                    {
                                        context.ErrorDescription = "Invalid access detected";
                                    }

                                    if (context.AuthenticateFailure is SecurityTokenExpiredException expirationException)
                                    {
                                        context.ErrorDescription = $"The token expired on {expirationException.Expires.ToString("o")}";
                                    }

                                    return context.Response.WriteAsJsonAsync(context.ErrorDescription);
                                }
                            };
                        });
             

            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

            services.AddScoped<TokenService>();

            return services;
        }
    }
}