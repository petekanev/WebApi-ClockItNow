namespace BillableHoursWebApp.DataTransferModels
{
    using System;

    public class CommentResponseModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public EmployeeResponseModel Employee { get; set; }

        public ClientResponseModel Client { get; set; }
    }
}
