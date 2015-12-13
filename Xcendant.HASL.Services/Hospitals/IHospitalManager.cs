using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.Services.Hospitals
{
    public interface IHospitalManager
    {

        Task<Hospital> FindRegisterdHospital(string userName);
        Task<int> RegisterNewHospitalOrUpdateDetails(Hospital registeredHospital);
    }
}