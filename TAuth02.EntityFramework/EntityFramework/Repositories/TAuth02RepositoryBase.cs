using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace TAuth02.EntityFramework.Repositories
{
    public abstract class TAuth02RepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<TAuth02DbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TAuth02RepositoryBase(IDbContextProvider<TAuth02DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class TAuth02RepositoryBase<TEntity> : TAuth02RepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TAuth02RepositoryBase(IDbContextProvider<TAuth02DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
