namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class EmployeesRepositoryMock
    {
        public static IRepository<Employee> Create()
        {
            var employeesList = new List<Employee>
            {
                new Employee
                {
                    Email = "TestEmployee@test.com",
                    UserName = "TestEmployee@test.com",
                    FirstName = "TestName",
                    Id = "qwerty123456"
                }
            };

            var repo = new Mock<IRepository<Employee>>();
            repo.Setup(x => x.All()).Returns(employeesList.AsQueryable());
            repo.Setup(x => x.Find(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeesList.AsQueryable());

            return repo.Object;
        }
    }
}
