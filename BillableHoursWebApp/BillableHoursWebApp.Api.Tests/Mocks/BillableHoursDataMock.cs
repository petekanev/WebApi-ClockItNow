namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using Data;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class BillableHoursDataMock
    {
        public static IBillableHoursWebAppData Create(IRepository<Project> projectsRepository, IRepository<Client> clientsRepository, IRepository<Employee> employeesRepository, IRepository<WorkLog> worklogsRepository, IRepository<Category> categoriesRepository)
        {
            var data = new Mock<IBillableHoursWebAppData>();
            data.Setup(x => x.SaveChanges()).Verifiable();
            data.SetupAllProperties();
            data.Object.Projects = projectsRepository;
            data.Object.Clients = clientsRepository;
            data.Object.Employees = employeesRepository;
            data.Object.WorkLogs = worklogsRepository;
            data.Object.Categories = categoriesRepository;

            return data.Object;
        }
    }
}
