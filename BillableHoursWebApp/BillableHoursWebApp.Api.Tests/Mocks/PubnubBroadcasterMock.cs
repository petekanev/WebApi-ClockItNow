namespace BillableHoursWebApp.Api.Tests.Mocks
{
    using System;
    using Common;
    using Moq;
    using PubNubMessaging.Core;

    public class PubnubBroadcasterMock
    {
        public static IPubnubBroadcaster Create()
        {
            var broadcaster = new Mock<IPubnubBroadcaster>();
            broadcaster.Setup(
                x =>
                    x.Broadcast(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<PubnubClientError>>(),
                        It.IsAny<Action<string>>())).Verifiable();

            return broadcaster.Object;
        }
    }
}
