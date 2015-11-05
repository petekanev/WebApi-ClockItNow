namespace BillableHoursWebApp.Data.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Employee
    {
        private ICollection<Project> projects;

        public Employee()
        {
            this.projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsCompany { get; set; }

        public virtual ICollection<Project> Projects 
        {
            get { return this.projects; }
            set { this.projects = value; } 
        }
    }
}
