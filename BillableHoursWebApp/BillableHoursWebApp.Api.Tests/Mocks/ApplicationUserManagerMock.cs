namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Moq;

    public class ApplicationUserManagerMock
    {
        public static ApplicationUserManager Create()
        {
            // create our mocked user
            User user = new Client { UserName = "TestClient@test.com", Email = "TestClient@test.com" };

            // mock the application user manager with mocked user store
            var mockedUserStore = new Mock<IUserStore<Client>>();
            var applicationUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            // mock the application user manager to always return our user object with any username and password
            applicationUserManager.Setup(x => x.FindAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            // mock the application user manager create identity in order to generate valid access token when requested
            applicationUserManager.Setup(x => x.CreateIdentityAsync(It.IsAny<Client>(), It.IsAny<string>()))
                .Returns<Client, string>(
                    (client, password) =>
                        Task.FromResult(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, client.UserName) },
                            DefaultAuthenticationTypes.ApplicationCookie)));

            return applicationUserManager.Object;
        }
    }
}
