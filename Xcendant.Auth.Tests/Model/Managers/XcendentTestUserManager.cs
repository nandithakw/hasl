﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xcendent.Auth.Models.DAL;
using Xcendent.Auth.Models.Entities;
using Xcendent.Auth.Models.Managers;

namespace Xcendant.Auth.Tests.Model.Managers
{
    /// <summary>
    /// This is a fake user manger for testing purpose.This uses an in-memoery dictionary to store user.
    /// </summary>
    class XcendentTestUserManager : AbstractXcendentUserManager<XcendentUser>
    {
        public Dictionary<string, XcendentUser> Users = new Dictionary<string, XcendentUser>();

        public XcendentTestUserManager(AbstractXcendentUserStore<XcendentUser, AbstractXcendentAuthContext> store, IdentityFactoryOptions<AbstractXcendentUserManager<XcendentUser>> options)
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


        //public override async Task<IdentityResult> CreateAsync(XcendentUser user, string password)
        //{
        //    IdentityResult result;
        //    await Task.Run(() =>
        //    {
        //        XcendentUser exsistingUser;
        //        if(this.Users.TryGetValue(user.UserName, out exsistingUser)){
        //        }
        //        else
        //        {

        //        }
        //    });

        //}
        public override async Task<ClaimsIdentity> GenerateUserIdentityAsync(XcendentUser user, string authenticationType)
        {
            throw new NotImplementedException();
        }
    }
}
