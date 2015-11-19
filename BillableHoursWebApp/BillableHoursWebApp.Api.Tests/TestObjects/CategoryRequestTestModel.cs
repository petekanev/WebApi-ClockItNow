namespace BillableHoursWebApp.Api.Tests.TestObjects
{
    using System;
    using DataTransferModels;

    public class CategoryRequestTestModel
    {
        public static CategoryRequestModel Create()
        {
            var model = new CategoryRequestModel
            {
                Name = "Test Category Name1234124"
            };

            return model;
        }
    }
}
