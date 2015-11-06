namespace BillableHoursWebApp.DataTransferModels
{
    using Common.Mapping;
    using Data.Models;

    public class AttachmentResponseModel : IMapFrom<Attachment>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
