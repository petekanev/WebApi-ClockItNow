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

        public BillableHoursWebAppData(IBillableHoursWebAppDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();

            this.Projects = new Repository<Project>(this.context);
            this.WorkLogs = new Repository<WorkLog>(this.context);
            this.Invoices = new Repository<Invoice>(this.context);
            this.Categories = new Repository<Category>(this.context);
            this.Comments = new Repository<Comment>(this.context);
            this.Attachments = new Repository<Attachment>(this.context);
        }

        public BillableHoursWebAppData()
            : this(new BillableHoursWebAppDbContext())
        {
        }

        public IRepository<Project> Projects { get; set; }

        public IRepository<WorkLog> WorkLogs { get; set; }

        public IRepository<Invoice> Invoices { get; set; }

        public IRepository<Category> Categories { get; set; }

        public IRepository<Comment> Comments { get; set; }

        public IRepository<Attachment> Attachments { get; set; }

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

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, new object[] { this.context }));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
