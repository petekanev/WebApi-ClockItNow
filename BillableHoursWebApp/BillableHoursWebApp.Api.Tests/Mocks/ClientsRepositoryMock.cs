namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class ClientsRepositoryMock
    {
        public static IRepository<Client> Create()
        {
            var clientsList = new List<Client>
            {
                new Client
                {
                    Email = "TestClient@test.com",
                    UserName = "TestClient@test.com",
                    FirstName = "TestName"
                }
            };

            var repo = new Mock<IRepository<Client>>();
            repo.Setup(x => x.All()).Returns(clientsList.AsQueryable());

            return repo.Object;
        }
    }
}
