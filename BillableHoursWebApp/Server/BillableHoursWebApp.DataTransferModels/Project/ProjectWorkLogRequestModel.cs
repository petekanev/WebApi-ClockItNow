namespace BillableHoursWebApp.DataTransferModels.Project
{
    using System;

    public class ProjectWorkLogRequestModel
    {
        public int ProjectId { get; set; }

        public string ShortDescription { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndDate { get; set; }

        // may prove to be unnecessary, as lots of other chunks of code
        public string EmployeeId { get; set; }
    }
}
