using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IS4
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        #region PrivateFields
        private SQLDbContext _ctx;
        //private readonly ILogServiceManager _logManager;
        private readonly IConfiguration _config;
        private string _apiClient;
        #endregion

        public ResourceOwnerPasswordValidator(IConfiguration config/*, ILogServiceManager logManager*/)
        {
            _config = config;
            //_logManager = logManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            _apiClient = context.Request.ValidatedResources.Resources.ApiScopes.ToList()[0].Name;
            if (string.IsNullOrEmpty(_apiClient)) return;

            try
            {
                var user = await GetUserByEmailAsync(context.UserName);
                if (user == null || IDS4HashUtils.VerifyPassword(context.Password, user.Password) != true)
                {
                    context.Result =
                        new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "Invalid username or password!"
                        );
                    return;
                }
                else if (user.IsConfirmed == false)
                {
                    context.Result =
                        new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "Email is not confirmed!"
                        );
                    return;
                }
                else if (user.TypeOfUser == TypeOfUser.Cancelled.ToString())
                {
                    context.Result =
                        new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "This user is cancelled! Unable to log in."
                        );
                    return;
                }

                context.Result =
                    new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: "custom",
                        claims: GetUserClaims(user)
                    );
            }
            catch (Exception ex)
            {
                var ce = new CustomException(ex);
                //await _logManager.LogCustomExceptionAsync(ce);
                context.Result =
                    new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        $"Server error! {ce.ClientErrorMessage}"
                    );
            }
        }

        public static IEnumerable<Claim> GetUserClaims(UserView user)
        {
            return user.Roles.Count switch
            {
                1 =>
                new Claim[]
                {
                    new Claim("user_id", user.Id.ToString() ?? ""),
                    new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                    new Claim(JwtClaimTypes.Role, user.Roles[0].RoleName.ToString())
                },
                2 =>
                new Claim[]
                {
                    new Claim("user_id", user.Id.ToString() ?? ""),
                    new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                    new Claim(JwtClaimTypes.Role, user.Roles[0].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[1].RoleName.ToString())
                },
                3 =>
                new Claim[]
                {
                    new Claim("user_id", user.Id.ToString() ?? ""),
                    new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                    new Claim(JwtClaimTypes.Role, user.Roles[0].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[1].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[2].RoleName.ToString())
                },
                4 =>
                new Claim[]
                {
                    new Claim("user_id", user.Id.ToString() ?? ""),
                    new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                    new Claim(JwtClaimTypes.Role, user.Roles[0].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[1].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[2].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[3].RoleName.ToString())
                },
                5 =>
                new Claim[]
                {
                    new Claim("user_id", user.Id.ToString() ?? ""),
                    new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                    new Claim(JwtClaimTypes.Role, user.Roles[0].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[1].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[2].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[3].RoleName.ToString()),
                    new Claim(JwtClaimTypes.Role, user.Roles[4].RoleName.ToString())
                },
                _ => throw new Exception("User should have at least one role assigned!"),
            };
        }

        private async Task<UserView> GetUserByEmailAsync(string email)
        {
            _ctx = SQLDbContextHelper.SetDbContext(_apiClient, _config);
            var rawUser = await _ctx.Users
                   .Include(u => u.Roles)
                   .ThenInclude(r => r.Role)
                   .FirstOrDefaultAsync(u => u.Email == email);
            return (UserView)rawUser ?? null;
        }

    }
}
