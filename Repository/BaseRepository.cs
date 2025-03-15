using Contracts.Base;
using Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Repository
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class,ISoftDeletedModel
    {
        private readonly FoeMaintainContext context;

        public BaseRepository(FoeMaintainContext context)
        {
            this.context = context;
        }

        public async Task Create(T entity) => await context.Set<T>().AddAsync(entity);


        
        

        public IQueryable<T> FindAll(bool trackchanges) => trackchanges ? context.Set<T>() : context.Set<T>().AsNoTracking();



        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackchanges)
            => trackchanges ? context.Set<T>().Where(expression) : context.Set<T>().Where(expression).AsNoTracking();

       
        public void HardDelete(T entity) => context.Set<T>().Remove(entity);
        

        public void SoftDelete(T entity) => entity.IsDeleted = true;
       
        public void Update(T entity) => context.Set<T>().Update(entity);
       
    }
}
