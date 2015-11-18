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
        }

        public BillableHoursWebAppData()
            : this(new BillableHoursWebAppDbContext())
        {
        }

        public IRepository<Client> Clients
        {
            get { return this.GetRepository<Client>(); }
            set { }
        }

        public IRepository<Employee> Employees
        {
            get { return this.GetRepository<Employee>(); }
            set { }
        }

        public IRepository<Project> Projects
        {
            get { return this.GetRepository<Project>(); }
            set { }
        }

        public IRepository<WorkLog> WorkLogs
        {
            get { return this.GetRepository<WorkLog>(); }
            set { }
        }

        public IRepository<Invoice> Invoices
        {
            get { return this.GetRepository<Invoice>(); }
            set { }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
            set { }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
            set { }
        }

        public IRepository<Attachment> Attachments
        {
            get { return this.GetRepository<Attachment>(); }
            set { }
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
                var type = typeof(Repository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
