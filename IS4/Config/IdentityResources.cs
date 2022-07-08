
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IS4
{
    public static class IdentityResourcesExtensions
    {
        public static IEnumerable<IdentityResource> GetIdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
    }
}
