namespace BillableHoursWebApp.DataTransferModels
{
    using System;
    using System.Collections.Generic;
    using Common.Mapping;
    using Data.Models;

    public class InvoiceResponseModel : IMapFrom<Invoice>
    {
        public int Id { get; set; }

        // consider referencing a Project Object
        public string ProjectTitle { get; set; }

        public DateTime IssuedOn { get; set; }

        // consider referencing an Employee Object
        public string EmployeeName { get; set; }

        // consider referencing a Client Object
        public string ClientName { get; set; }

        public CategoryResponseModel Category { get; set; }

        public IEnumerable<WorkLogResponseModel> WorkLogs { get; set; } 
    }
}
