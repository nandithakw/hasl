using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.Members
{
    public interface IRegisteredUserFacade
    {
        Task<RegisteredUser> GetUserDetails(IHaslContext iHaslContext, string email);
        Task<int> RegisterNewUserDetails(IHaslContext iHaslContext, RegisteredUser userDetails);
        Task<int> UpdateUserDetails(IHaslContext iHaslContext, RegisteredUser userDetails);
    }
}
