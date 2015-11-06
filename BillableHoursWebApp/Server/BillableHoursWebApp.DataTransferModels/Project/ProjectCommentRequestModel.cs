namespace BillableHoursWebApp.DataTransferModels.Project
{
    using System;

    public class ProjectCommentRequestModel
    {
        public int ProjectId { get; set; }

        public string CommentContent { get; set; }

        public string EmployeeId { get; set; }

        public string ClientId { get; set; }
    }
}
