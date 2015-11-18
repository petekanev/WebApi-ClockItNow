namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class ProjectsRepositoryMock
    {
        public static IRepository<Project> Create()
        {
            var projectsList = new List<Project>();
            var categories = new List<Category>
            {
                new Category {Id = 1, Name = "Mock Category"},
                new Category {Id = 5, Name = "Another Mock Category"}
            };

            for (int i = 1; i <= 20; i++)
            {
                projectsList.Add(new Project
                {
                    TimePublished = DateTime.Now,
                    Name = "Mock Project version" + i,
                    Description = i + "Mock Project Description " + new string('*', 50 + i),
                    Id = i,
                    PricePerHour = 30m,
                    Category = categories[i % categories.Count]
                });
            }

            var repo = new Mock<IRepository<Project>>();
            repo.Setup(x => x.All()).Returns(projectsList.AsQueryable());
            repo.Setup(x => x.Add(It.IsAny<Project>())).Callback<Project>(p => { projectsList.Add(p); });

            return repo.Object;
        }
    }
}
