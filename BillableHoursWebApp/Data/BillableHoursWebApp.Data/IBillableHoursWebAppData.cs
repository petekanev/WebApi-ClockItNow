namespace BillableHoursWebApp.Data
{
    using Models;
    using Repositories;

    public interface IBillableHoursWebAppData
    {
        IRepository<Employee> Employees { get; set; }

        IRepository<Client> Clients { get; set; }

        IRepository<Project> Projects { get; }

        IRepository<WorkLog> WorkLogs { get; }

        IRepository<Invoice> Invoices { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Attachment> Attachments { get; }

        void SaveChanges();
    }
}
