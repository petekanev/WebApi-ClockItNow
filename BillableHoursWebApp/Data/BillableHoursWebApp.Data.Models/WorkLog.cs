namespace BillableHoursWebApp.Data.Models
{
    using System;

    public class WorkLog
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
