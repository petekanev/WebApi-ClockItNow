namespace BillableHoursWebApp.Api.Tests.TestObjects
{
    using System;
    using DataTransferModels.Project;

    public class ProjectWorkLogRequestTestModel
    {
        public static ProjectWorkLogRequestModel Create()
        {
            var model = new ProjectWorkLogRequestModel
            {
                ShortDescription = "Short log description"
            };

            return model;
        }
    }
}
