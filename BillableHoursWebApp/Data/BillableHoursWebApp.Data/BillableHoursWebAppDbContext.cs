namespace BillableHoursWebApp.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class BillableHoursWebAppDbContext 
        : IdentityDbContext<User>, 
        IBillableHoursWebAppDbContext
    {
        public static BillableHoursWebAppDbContext Create()
        {
            return new BillableHoursWebAppDbContext();
        }
    }
}
