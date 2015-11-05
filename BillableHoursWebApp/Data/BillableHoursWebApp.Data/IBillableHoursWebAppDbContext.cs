namespace BillableHoursWebApp.Data
{
    using System.Data.Entity;
    using Models;

    public interface IBillableHoursWebAppDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Attachment> Attachments { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<WorkLog> WorkLogs { get; set; }

        IDbSet<Invoice> Invoices { get; set; }

        void SaveChanges();
    }
}
