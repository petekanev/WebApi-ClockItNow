namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class BillableHoursDataMock
    {
        public static IBillableHoursWebAppData Create(IRepository<Project> projectsRepository)
        {
            var data = new Mock<IBillableHoursWebAppData>();
            data.Setup(x => x.SaveChanges()).Callback(() => { });
            data.SetupAllProperties();
            data.Object.Projects = projectsRepository;

            return data.Object;
        }
    }
}
