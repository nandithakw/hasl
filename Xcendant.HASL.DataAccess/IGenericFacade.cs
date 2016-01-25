using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xcendant.HASL.DataAccess
{
    public interface IGenericFacade<T> where T : class
    {
        Task<T> GetDetails<U>(IHaslContext iHaslContext, U key);
        Task<IEnumerable<T>> GetResultsWithRawSqlAsync(IHaslContext iHaslContext, string query, params object[] parameters);
        Task<T> GetResultWithRawSqlAsync(IHaslContext iHaslContext, string query, params object[] parameters);
        Task<int> AddNew(IHaslContext iHaslContext, T entitiy);
        Task<int> UpdateAsync(IHaslContext iHaslContext, T entitiy);
        Task<int> Delete(IHaslContext iHaslContext, T entitiy);
        Task<List<T>> GetAllAsync(IHaslContext iHaslContext);

    }
}