namespace BillableHoursWebApp.DataTransferModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryRequestModel
    {
        [Required]
        public string Name { get; set; }
    }
}
