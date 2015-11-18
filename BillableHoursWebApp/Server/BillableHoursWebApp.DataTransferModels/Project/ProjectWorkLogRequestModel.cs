namespace BillableHoursWebApp.DataTransferModels.Project
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProjectWorkLogRequestModel
    {
        public int ProjectId { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string ShortDescription { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
