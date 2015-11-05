namespace BillableHoursWebApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        private ICollection<WorkLog> logs;
        private ICollection<Attachment> attachments;
        private ICollection<Comment> comments;

        public Project()
        {
            this.logs = new HashSet<WorkLog>();
            this.attachments = new HashSet<Attachment>();
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public DateTime TimePublished { get; set; }

        public DateTime? TimeFinished { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsComplete { get; set; }

        public decimal PricePerHour { get; set; }

        public virtual ICollection<Attachment> Attachments
        {
            get { return this.attachments; }
            set { this.attachments = value; }
        }

        public virtual ICollection<WorkLog> WorkLogs
        {
            get { return this.logs; }
            set { this.logs = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
