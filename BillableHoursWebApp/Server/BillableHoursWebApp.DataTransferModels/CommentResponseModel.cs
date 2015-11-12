namespace BillableHoursWebApp.DataTransferModels
{
    using System;
    using Common.Mapping;
    using Data.Models;

    public class CommentResponseModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public EmployeeResponseModel Employee { get; set; }

        public ClientResponseModel Client { get; set; }
    }
}
