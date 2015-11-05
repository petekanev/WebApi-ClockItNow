namespace BillableHoursWebApp.Data.Models
{
    using System;

    public class Category
    {
        public int Id { get; set; }

        // [Required]
        // IsUnique
        public string Name { get; set; }
    }
}
