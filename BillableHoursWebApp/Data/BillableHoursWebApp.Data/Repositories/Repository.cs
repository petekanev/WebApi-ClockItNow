namespace BillableHoursWebApp.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using Data;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private IBillableHoursWebAppDbContext context;
        private IDbSet<T> set;

        public Repository(IBillableHoursWebAppDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set.AsQueryable();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> conditions)
        {
            return this.All().Where(conditions);
        }

        public T GetById(object id)
        {
            return this.All().FirstOrDefault(x => x.Equals(id));
        }

        public void Add(T entity)
        {
            this.set.Add(entity);
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public void Detach(T entity)
        {
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            return entry;
        }
    }
}
