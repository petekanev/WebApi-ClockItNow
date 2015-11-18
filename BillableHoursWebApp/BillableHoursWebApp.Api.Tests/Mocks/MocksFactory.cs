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
            get { return BillableHoursDataMock.Create(ProjectsRepository); }
        }
    }
}
