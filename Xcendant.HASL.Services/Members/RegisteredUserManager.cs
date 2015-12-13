using System;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;
using Xcendant.HASL.DataAccess.Members;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Members
{
    public class RegisteredUserManager : IRegisteredUserManager
    {
        public IRegisteredUserFacade IRegisteredUserFacade { get; set; }
        Func<Owned<IHaslContext>> IHaslContext { get; set; }

        public RegisteredUserManager(Func<Owned<IHaslContext>> iHaslContext, IRegisteredUserFacade iRegisterdMembersFacade)
        {
            IHaslContext = iHaslContext;
            IRegisteredUserFacade = iRegisterdMembersFacade;
        }



        public async Task<RegisteredUser> FindRegisterdUser(string userName)
        {
            RegisteredUser registeredUser = null;
            using (var iHaslContext = IHaslContext())
            {
                registeredUser = await IRegisteredUserFacade.GetUserDetails(iHaslContext.Value, userName);
            }


            return registeredUser;

        }

        public async Task<int> RegisterNewUserOrUpdateDetails(RegisteredUser registeredUser)
        {
            int modifiedCount = -1;
            if (!"LK".Equals(registeredUser.Country))
            {
                registeredUser.District = null;
                registeredUser.Province = null;
            }
            registeredUser.DateOfBirth = registeredUser.DateOfBirth.Date;
            using (var iHaslContext = IHaslContext())
            {
                RegisteredUser user = await IRegisteredUserFacade.GetUserDetails(iHaslContext.Value, registeredUser.Email);
                if (user == null)
                {
                    modifiedCount = await IRegisteredUserFacade.RegisterNewUserDetails(iHaslContext.Value, registeredUser);

                }
                else
                {
                    user.FirstName = registeredUser.FirstName;
                    user.LastName = registeredUser.LastName;
                    user.AddressLine01 = registeredUser.AddressLine01;
                    user.AddressLine02 = registeredUser.AddressLine02;
                    user.City = registeredUser.City;
                    user.District = registeredUser.District;
                    user.Province = registeredUser.Province;
                    user.Country = registeredUser.Country;
                    user.HomeNumber = registeredUser.HomeNumber;
                    user.WorkNumber = registeredUser.WorkNumber;
                    user.MobileNumber = registeredUser.MobileNumber;
                    user.IdentificaionType = registeredUser.IdentificaionType;
                    user.IdentificationNumber = registeredUser.IdentificationNumber;
                    user.Gender = registeredUser.Gender;
                    user.DateOfBirth = registeredUser.DateOfBirth;

                    modifiedCount = await IRegisteredUserFacade.UpdateUserDetails(iHaslContext.Value, user);

                }
            }
            return modifiedCount;
        }


    }
}
