namespace BillableHoursWebApp.Data.Models
{
    using System;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        // Either can be the author of a comment
        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
