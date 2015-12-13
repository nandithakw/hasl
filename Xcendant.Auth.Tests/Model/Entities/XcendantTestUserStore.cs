using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xcendant.Auth.Models.Entities;
namespace Xcendant.Auth.Tests.Model.Entities
{
    public class XcendantTestUserStore<TUser> : IUserStore<TUser> where TUser : XcendentUser
    {

        public Task CreateAsync(TUser user)
        {
            throw new NotImplementedException("Please use nsubstitute");
        }

        public Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException("Please use nsubstitute");
        }

        public void Dispose()
        {
            throw new NotImplementedException("Please use nsubstitute");
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException("Please use nsubstitute");
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException("Please use nsubstitute");
        }

        public Task UpdateAsync(TUser user)
        {
            throw new NotImplementedException("Please use nsubstitute");
        }
    }
}
