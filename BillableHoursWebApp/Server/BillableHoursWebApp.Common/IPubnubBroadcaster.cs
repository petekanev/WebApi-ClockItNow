namespace BillableHoursWebApp.Common
{
    using System;
    using PubNubMessaging.Core;

    public interface IPubnubBroadcaster
    {
        void Broadcast(string channel, string message, Action<PubnubClientError> errorCallback, Action<string> userCallback);
    }
}
