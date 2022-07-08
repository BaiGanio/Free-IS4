using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IS4
{
    public class ProfileService : IProfileService
    {
        #region PrivateFields
        private SQLDbContext _ctx;
        private readonly IConfiguration _config;
       // private readonly ILogServiceManager _logger;
        private string _apiClient;
        #endregion

        public ProfileService(IConfiguration config)
        {
            _config = config;
            //this._logger = new LogManager(config, null);
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                _apiClient = context.RequestedResources.Resources.ApiScopes.ToList()[0].Name;
                if (string.IsNullOrEmpty(_apiClient)) return;

                _ctx = GetDbContext();
                UserView user;
                string userEmail = context.Subject.Claims.FirstOrDefault(c => c.Type == "email" || c.Type == "name").Value;

                if (string.IsNullOrEmpty(userEmail))
                {
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (string.IsNullOrEmpty(userId?.Value))
                        throw new Exception("Can't extract user Id from request context!");
                    else
                    {
                        user = await GetUserByIdAsync(userId.Value);
                    }
                }
                else
                {
                    user = await GetUserByEmailAsync(userEmail);
                }

                if (user == null) return;
                SetIssuedClaims(context, ResourceOwnerPasswordValidator.GetUserClaims(user));
            }
            catch (Exception)
            {
                //await _logger.LogExceptionAsync(ex);
                throw;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                // TODO: Lyuben Kikov - this looks quite not right. Claim could be of type [user_id]
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    _apiClient = context.Client.AllowedScopes.ToList()[0];
                    _ctx = GetDbContext();
                    var user = await _ctx.Users.FindAsync(userId.Value);

                    if (user != null)
                    {
                        context.IsActive = true;
                        //if (user.IsActive)
                        //{
                        //    // user.IsActive;
                        //}
                    }
                }
            }
            catch (Exception)
            {
                //await _logger.LogExceptionAsync(ex);
                throw;
            }
        }

        #region PrivateMethods

        private static void SetIssuedClaims(ProfileDataRequestContext context, IEnumerable<Claim> claims)
        {

var jj = claims
                        .Where(x => context.RequestedClaimTypes.Contains(x.Type))
                        .ToList();

            context.IssuedClaims = claims.ToList();
                     
        }
       
        private async Task<UserView> GetUserByIdAsync(string id)
        {
            var rawUser =
                await _ctx.Users
                .Include(u => u.Roles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            return (UserView)rawUser ?? null;
        }

        private async Task<UserView> GetUserByEmailAsync(string email)
        {
            var rawUser =
                await _ctx.Users
                .Include(u => u.Roles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            return (UserView)rawUser ?? null;
        }

        private SQLDbContext GetDbContext()
        {
            return SQLDbContextHelper.SetDbContext(_apiClient, _config);
        }

        #endregion PrivateMethods
    }
}
