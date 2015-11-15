namespace BillableHoursWebApp.DataTransferModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Mapping;
    using Data.Models;

    public class WorkLogResponseModel : IMapFrom<WorkLog>
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
