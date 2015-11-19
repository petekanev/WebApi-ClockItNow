namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using System.IO;
    using Common;
    using Moq;

    public class DropboxHelperMock
    {
        public static IDropboxHelper Create()
        {
            var mock = new Mock<IDropboxHelper>();
            mock.Setup(x => x.UploadFileEntry(It.IsAny<Stream>(), It.IsAny<string>()))
                .Returns("http://example.com/");

            return mock.Object;
        }
    }
}
