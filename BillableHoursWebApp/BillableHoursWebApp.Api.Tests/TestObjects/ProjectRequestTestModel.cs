namespace BillableHoursWebApp.Api.Tests.TestObjects
{
    using System;
    using System.Collections.Generic;
    using DataTransferModels;
    using DataTransferModels.Project;

    public class ProjectRequestTestModel
    {
        public static ProjectRequestModel Create()
        {
            var model = new ProjectRequestModel
            {
                Name = "This is another fake project",
                Description =
                    "TestDescriptionTestDescriptionTestDescription TestDescription TestDescription TestDescription TestDescription TestDescriptionTestDescriptionTestDescription TestDescription",
                CategoryId = 1,
                PricePerHour = 60,
                Attachments = new List<AttachmentRequestModel>
                {
                    new AttachmentRequestModel
                    {
                        Name = "Test name",
                        Url = "http://example.com"
                    }
                }
            };

            return model;
        }

        public static ProjectRequestModel CreateInvalid()
        {
            var model = new ProjectRequestModel
            {
                Name = "",
                Description =
                    "TestDescription",
                PricePerHour = -10,
                Attachments = new List<AttachmentRequestModel>
                {
                    new AttachmentRequestModel()
                }
            };

            return model;
        }
    }
}
