namespace BillableHoursWebApp.Data
{
    using Models;

    public interface IBillableHoursWebAppData
    {
        Repositories.IRepository<Project> Projects { get; }

        Repositories.IRepository<WorkLog> WorkLogs { get; }

        Repositories.IRepository<Invoice> Invoices { get; }

        Repositories.IRepository<Category> Categories { get; }

        Repositories.IRepository<Comment> Comments { get; }

        Repositories.IRepository<Attachment> Attachments { get; } 

        void SaveChanges();
    }
}
