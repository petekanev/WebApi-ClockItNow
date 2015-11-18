namespace BillableHoursWebApp.Common
{
    using System;
    using PubNubMessaging.Core;

    public class PubnubBroadcaster : IPubnubBroadcaster
    {
        private readonly Pubnub pubnubClient;

        public PubnubBroadcaster(string publishKey, string subscribeKey)
        {
            pubnubClient = new Pubnub(publishKey, subscribeKey);
        }

        public void Broadcast(string channel, string message, Action<PubnubClientError> errorCallback, Action<string> userCallback)
        {
            pubnubClient.Publish<string>(channel: channel, message: message, errorCallback:
                errorCallback, userCallback: userCallback);
        }
    }
}
