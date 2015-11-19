namespace BillableHoursWebApp.Api.Tests.TestObjects
{
    using System;
    using DataTransferModels.Project;

    public class TestObjectsFactory
    {
        public static ProjectRequestModel ValidProjectRequestModel
        {
            get { return ProjectRequestTestModel.Create(); }
        }

        public static ProjectRequestModel InvalidProjectRequestModel
        {
            get { return ProjectRequestTestModel.CreateInvalid(); }
        }

        public static ProjectWorkLogRequestModel ValidProjectWorkLogRequestModel
        {
            get { return ProjectWorkLogRequestTestModel.Create(); } 
        }
    }
}
