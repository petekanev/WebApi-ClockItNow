namespace BillableHoursWebApp.DataTransferModels.Project
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Common.Mapping;
    using Data.Models;

    public class ProjectResponseModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryResponseModel Category { get; set; }

        public EmployeeResponseModel Employee { get; set; }

        public ClientResponseModel Client {get; set; }

        public DateTime TimePublished { get; set; }

        public DateTime? TimeFinished { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsComplete { get; set; }

        public decimal PricePerHour { get; set; }

        public IEnumerable<AttachmentResponseModel> Attachments { get; set; }

        public IEnumerable<WorkLogResponseModel> WorkLogs { get; set; }

        public IEnumerable<CommentResponseModel> Comments { get; set; }
    }
}
