using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Xcendant.HASL.DataAccess
{
    public abstract class AbstractGenericFacade<T> : IGenericFacade<T> where T : class
    {
        public abstract Task<T> GetDetails<U>(IHaslContext iHaslContext, U key);

        public async Task<List<T>> GetAllAsync(IHaslContext iHaslContext)
        {
            var entities = await iHaslContext.Set<T>().AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<int> AddNew(IHaslContext iHaslContext, T entitiy)
        {
            iHaslContext.Set<T>().Add(entitiy);
            iHaslContext.Entry(entitiy).State = EntityState.Added;
            return await iHaslContext.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(IHaslContext iHaslContext, T entitiy)
        {
            iHaslContext.Entry(entitiy).State = EntityState.Modified;
            return await iHaslContext.SaveChangesAsync();
        }

        public async Task<int> Delete(IHaslContext iHaslContext, T entitiy)
        {
            iHaslContext.Entry(entitiy).State = EntityState.Deleted;
            return await iHaslContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetResultsWithRawSqlAsync(IHaslContext iHaslContext, string query, params object[] parameters)
        {
            return await iHaslContext.Set<T>().SqlQuery(query, parameters).AsNoTracking().ToListAsync();

        }

        public async Task<T> GetResultWithRawSqlAsync(IHaslContext iHaslContext, string query, params object[] parameters)
        {

            return await iHaslContext.Set<T>().SqlQuery(query, parameters).AsNoTracking().FirstOrDefaultAsync();

        }
    }
}
