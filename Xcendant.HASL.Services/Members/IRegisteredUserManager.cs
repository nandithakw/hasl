using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Members
{
    public interface IRegisteredUserManager
    {
        Task<RegisteredUser> FindRegisterdUser(string userName);
        Task<int> RegisterNewUserOrUpdateDetails(RegisteredUser registeredUser);

    }
}