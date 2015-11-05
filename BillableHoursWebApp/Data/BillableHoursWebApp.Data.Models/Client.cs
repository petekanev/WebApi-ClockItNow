namespace BillableHoursWebApp.Data.Models
{
    using System.Collections.Generic;

    public class Client
    {
        private ICollection<Project> projects;

        public Client()
        {
            this.projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsOrganization { get; set; }

        public virtual ICollection<Project> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }
    }
}
