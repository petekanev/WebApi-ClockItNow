namespace BillableHoursWebApp.DataTransferModels.Project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProjectRequestModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(1000)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string EmployeeId { get; set; }

        public string ClientId { get; set; }

        public DateTime TimePublished { get; set; }

        public DateTime? TimeFinished { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsComplete { get; set; }

        [Range(5, 300)]
        public decimal PricePerHour { get; set; }

        public IEnumerable<AttachmentRequestModel> Attachments { get; set; }
    }
}