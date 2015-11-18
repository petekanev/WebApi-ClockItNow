﻿namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
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
                new Category {Id = 2, Name = "Mock Category2"},
                new Category {Id = 3, Name = "Mock Category3"},
                new Category {Id = 4, Name = "Mock Category4"},
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
                    Category = categories[i % categories.Count],
                    CategoryId = categories[i % categories.Count].Id
                });
            }

            IQueryable<Project> projectsAsQueryable = projectsList.AsQueryable();

            var repo = new Mock<IRepository<Project>>();
            repo.Setup(x => x.All()).Returns(projectsAsQueryable);
            repo.Setup(x => x.Find(It.IsAny<Expression<Func<Project, bool>>>()))
                .Returns<Expression<Func<Project, bool>>>(id => projectsAsQueryable.Where(id));
            repo.Setup(x => x.Add(It.IsAny<Project>())).Callback<Project>(p => { projectsList.Add(p); });

            return repo.Object;
        }
    }
}
