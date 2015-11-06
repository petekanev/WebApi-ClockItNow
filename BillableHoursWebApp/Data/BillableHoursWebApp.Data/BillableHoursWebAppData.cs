namespace BillableHoursWebApp.Data
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Repositories;

    public class BillableHoursWebAppData : IBillableHoursWebAppData
    {
        private readonly IBillableHoursWebAppDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public IRepository<Project> Projects
        {
            get { return this.GetRepository<Project>(); }
        }

        public IRepository<WorkLog> WorkLogs
        {
            get { return this.GetRepository<WorkLog>(); }
        }

        public IRepository<Invoice> Invoices
        {
            get { return this.GetRepository<Invoice>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Attachment> Attachments
        {
            get { return this.GetRepository<Attachment>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(IRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
