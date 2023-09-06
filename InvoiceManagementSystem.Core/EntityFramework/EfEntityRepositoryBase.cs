using InvoiceManagementSystem.Core.Entities;
using InvoiceManagementSystem.Core.Pagination;
using InvoiceManagementSystem.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Core.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity,new()
        where TContext:DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context=new TContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void AddMultiple(List<TEntity> entity)
        {
            using (var context = new TContext())
            {
               
                    var addEntity = context.Entry(entity);
                    addEntity.State = EntityState.Added;
                    context.SaveChanges();
                
               
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context=new TContext())
            {
                var delEntity = context.Entry(entity);
                delEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context=new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }


        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context=new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }



        //public PagingResponseDto<TEntity> GetPagingList(PagingRequestDto pagingRequestDto, Expression<Func<TEntity, object>> orderBy = null, Expression<Func<TEntity, object>> thenOrderBy = null, bool isDesc = false, List<string> searchTypes = null, IDictionary<string, string> stringParameters = null, IDictionary<string, int?> intParameters = null, IDictionary<string, bool?> boolParameters = null, IDictionary<string, byte?> byteParameters = null, List<KeyValueDto> dateTimeParameters = null, List<KeyValueParameterDto> parameters = null, string search = null)
        //{
        //   Type type = typeof(TEntity);
        //    var types = type.GetProperties();
        //}
        
        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
