namespace BillableHoursWebApp.DataTransferModels
{
    using System.ComponentModel.DataAnnotations;

    public class AttachmentRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
