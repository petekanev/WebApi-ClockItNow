namespace BillableHoursWebApp.Common
{
    using System.IO;

    public interface IDropboxHelper
    {
        string UploadFileEntry(Stream stream, string cloudDirName = "/Invoices/");
    }
}
