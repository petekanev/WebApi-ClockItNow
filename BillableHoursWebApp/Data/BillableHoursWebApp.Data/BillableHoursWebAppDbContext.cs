namespace BillableHoursWebApp.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class BillableHoursWebAppDbContext : IdentityDbContext<User>, IBillableHoursWebAppDbContext
    {
        public BillableHoursWebAppDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Attachment> Attachments { get; set; }

        public virtual IDbSet<Project> Projects { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<WorkLog> WorkLogs { get; set; }

        public virtual IDbSet<Invoice> Invoices { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public static BillableHoursWebAppDbContext Create()
        {
            return new BillableHoursWebAppDbContext();
        }
    }
}
