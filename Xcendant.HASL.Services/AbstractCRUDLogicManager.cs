using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Xcendant.HASL.DataAccess;

namespace Xcendant.HASL.Services
{
    public class AbstractCRUDLogicManager<TEntity, TIEntityFacade> : ICRUDLogicManager<TEntity, TIEntityFacade>
        where TEntity : class
        where TIEntityFacade : IGenericFacade<TEntity>
    {


        protected TIEntityFacade iEntityFacade { get; set; }
        protected Func<Owned<IHaslContext>> iHaslContext { get; set; }

        public AbstractCRUDLogicManager(Func<Owned<IHaslContext>> iHaslContext, TIEntityFacade tEntityFacade)
        {
            this.iHaslContext = iHaslContext;
            this.iEntityFacade = tEntityFacade;
        }



        public virtual async Task<TEntity> FindAsync<Tkey>(Tkey key)
        {
            TEntity entity = null;
            using (var ctx = this.iHaslContext())
            {
                entity = await this.iEntityFacade.GetDetails(ctx.Value, key);
            }


            return entity;

        }


        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = null;
            using (var ctx = this.iHaslContext())
            {
                entities = await this.iEntityFacade.GetAllAsync(ctx.Value);
            }


            return entities;
        }

        public virtual async Task<int> RegisterNewOrUpdateDetailsAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
