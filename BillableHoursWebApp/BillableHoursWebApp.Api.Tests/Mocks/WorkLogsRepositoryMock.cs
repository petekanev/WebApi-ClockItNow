namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Data.Models;
    using Data.Repositories;
    using Moq;

    public class WorkLogsRepositoryMock
    {
        public static IRepository<WorkLog> Create()
        {
            var logsList = new List<WorkLog>();

            for (int i = 1; i < 20; i++)
            {
                logsList.Add(new WorkLog
                {
                    Id = i,
                    ShortDescription = "Log short Description " + i,
                    StartTime = DateTime.Now
                });
            }

            logsList.Add(new WorkLog
            {
                Id = 25,
                ShortDescription = "Log short Description " + 25,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });

            var repo = new Mock<IRepository<WorkLog>>();
            repo.Setup(x => x.All()).Returns(logsList.AsQueryable());
            repo.Setup(x => x.Find(It.IsAny<Expression<Func<WorkLog, bool>>>()))
                .Returns<Expression<Func<WorkLog, bool>>>(id => logsList.AsQueryable().Where(id));
            repo.Setup(x => x.Update(It.IsAny<WorkLog>())).Verifiable();

            return repo.Object;
        }
    }
}
