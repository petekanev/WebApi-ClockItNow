namespace BillableHoursWebApp.Common
{
    using System;
    using System.IO;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using Spring.Social.OAuth1;

    public class DropboxHelper
    {
        private DropboxServiceProvider serviceProvider;
        private IDropbox dropbox;

        public DropboxHelper()
        {
            this.serviceProvider = new DropboxServiceProvider(Constants.DropboxAppKey, Constants.DropboxAppSecret, AccessLevel.AppFolder);
            this.dropbox = serviceProvider.GetApi(Constants.DropboxAppToken, Constants.DropboxAppTokenSecret);
        }

        public string UploadFileEntry(Stream stream, string cloudDirName = "/Invoices/")
        {
            IResource res = new StreamResource(stream);
            var fileEntry = this.dropbox.UploadFileAsync(res, cloudDirName).Result;

            var shareableLink = dropbox.GetShareableLinkAsync(fileEntry.Path).Result;
            return shareableLink.Url;
        }
    }
}
