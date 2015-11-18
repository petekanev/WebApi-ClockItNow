namespace BillableHoursWebApp.Data
{
    using Models;
    using Repositories;

    public interface IBillableHoursWebAppData
    {
        IRepository<Employee> Employees { get; set; }

        IRepository<Client> Clients { get; set; }

        IRepository<Project> Projects { get; set; }

        IRepository<WorkLog> WorkLogs { get; set; }

        IRepository<Invoice> Invoices { get; set; }

        IRepository<Category> Categories { get; set; }

        IRepository<Comment> Comments { get; set; }

        IRepository<Attachment> Attachments { get; set; }

        void SaveChanges();
    }
}
