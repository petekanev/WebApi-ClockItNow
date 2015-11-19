namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class CategoriesRepositoryMock
    {
        public static IRepository<Category> Create()
        {
            var categoriesList = new List<Category>            
            {
                new Category {Id = 1, Name = "Mock Category"},
                new Category {Id = 2, Name = "Mock Category2"},
                new Category {Id = 3, Name = "Mock Category3"},
                new Category {Id = 4, Name = "Mock Category4"},
                new Category {Id = 5, Name = "Another Mock Category"}
            };

            var repo = new Mock<IRepository<Category>>();
            repo.Setup(x => x.All()).Returns(categoriesList.AsQueryable());
            repo.Setup(x => x.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns<Expression<Func<Category, bool>>>(expression => categoriesList.AsQueryable().Where(expression));
            repo.Setup(x => x.Add(It.IsAny<Category>())).Callback<Category>(c =>
            {
                c.Id = categoriesList.Last().Id + 1;
                categoriesList.Add(c);
            });
            repo.Setup(x => x.Delete(It.IsAny<Category>())).Verifiable();

            return repo.Object;
        }
    }
}
