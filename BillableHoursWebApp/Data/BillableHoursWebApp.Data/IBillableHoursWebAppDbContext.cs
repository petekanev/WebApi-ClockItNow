namespace BillableHoursWebApp.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;

    public interface IBillableHoursWebAppDbContext
    {
        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        IDbSet<Category> Categories { get; set; }

        IDbSet<Attachment> Attachments { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<WorkLog> WorkLogs { get; set; }

        IDbSet<Invoice> Invoices { get; set; }

        void SaveChanges();
    }
}
