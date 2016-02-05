using System.Collections.Generic;
using System.Threading.Tasks;
using Xcendant.HASL.DataAccess;

namespace Xcendant.HASL.Services
{
    public interface ICRUDLogicManager<TEntity, TIEntityFacade>
        where TEntity : class
        where TIEntityFacade : IGenericFacade<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync<TKey>(TKey key);
        Task<int> RegisterNewOrUpdateDetailsAsync(TEntity entity);

    }
}
