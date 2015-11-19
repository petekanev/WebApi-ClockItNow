namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using Common;
    using Data;
    using Data.Models;
    using Data.Repositories;

    public class MocksFactory
    {
        public static IRepository<Project> ProjectsRepository
        {
            get { return ProjectsRepositoryMock.Create(); }
        }

        public static IRepository<Client> ClientsRepository
        {
            get { return ClientsRepositoryMock.Create(); }
        }

        public static IRepository<Employee> EmployeesRepository
        {
            get { return EmployeesRepositoryMock.Create(); }
        }

        public static IRepository<WorkLog> WorkLogsRepository
        {
            get { return WorkLogsRepositoryMock.Create(); }
        }

        public static IRepository<Category> CategoriesRepository
        {
            get { return CategoriesRepositoryMock.Create(); }
        }

        public static ApplicationUserManager ApplicationUserManager
        {
            get { return ApplicationUserManagerMock.Create(); }
        }

        public static IPubnubBroadcaster PubnubBroadcaster
        {
            get { return PubnubBroadcasterMock.Create(); }
        }

        public static IBillableHoursWebAppData BillableHoursWebAppData
        {
            get { return BillableHoursDataMock.Create(ProjectsRepository, ClientsRepository, EmployeesRepository, WorkLogsRepository, CategoriesRepository); }
        }

        public static IDropboxHelper DropboxHelper
        {
            get { return DropboxHelperMock.Create(); }
        }
    }
}
