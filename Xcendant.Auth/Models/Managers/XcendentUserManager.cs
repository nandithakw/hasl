using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Xcendant.Auth.Models.DAL;
using Xcendant.Auth.Models.Entities;

namespace Xcendant.Auth.Models.Managers
{
    public class XcendentUserManager : AbstractXcendentUserManager<XcendentUser>
    {
        public XcendentUserManager(AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext> store, IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>> options)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<XcendentUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                this.UserTokenProvider = new DataProtectorTokenProvider<XcendentUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }



        public override async Task<ClaimsIdentity> GenerateUserIdentityAsync(XcendentUser user, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await this.CreateIdentityAsync(user, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }
}