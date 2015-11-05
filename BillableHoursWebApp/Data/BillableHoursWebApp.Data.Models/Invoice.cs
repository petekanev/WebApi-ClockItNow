namespace BillableHoursWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Invoice
    {
        private ICollection<WorkLog> logs;

        public Invoice()
        {
            this.logs = new HashSet<WorkLog>();
        }

        public int Id { get; set; }

        // consider referencing a Project Object
        public string ProjectTitle { get; set; }

        public DateTime IssuedOn { get; set; }

        // consider referencing an Employee Object
        public string EmployeeName { get; set; }

        // consider referencing a Client Object
        public string ClientName { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<WorkLog> WorkLogs
        {
            get { return this.logs; }
            set { this.logs = value; }
        }
    }
}
