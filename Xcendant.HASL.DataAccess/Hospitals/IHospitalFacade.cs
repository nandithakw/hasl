using System.Threading.Tasks;
using Xcendant.HASL.Entities;

namespace Xcendant.HASL.DataAccess.Hospitals
{
    public interface IHospitalFacade
    {
        Task<int> DeleteHospital(IHaslContext iHaslContext, Hospital hospital);
        Task<Hospital> GetHospitalDetails(IHaslContext iHaslContext, string email);
        Task<int> RegisterNewHospital(IHaslContext iHaslContext, Hospital hospital);
        Task<int> UpdateHospital(IHaslContext iHaslContext, Hospital hospital);
    }
}