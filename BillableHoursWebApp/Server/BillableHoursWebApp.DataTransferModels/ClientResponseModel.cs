namespace BillableHoursWebApp.DataTransferModels
{
    using System.Collections.Generic;
    using Common.Mapping;
    using Data.Models;
    using Project;

    public class ClientResponseModel : IMapFrom<Client>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsOrganization { get; set; }

        public IEnumerable<ProjectResponseModel> Projects { get; set; }

        public IEnumerable<InvoiceResponseModel> Invoices { get; set; } 
    }
}
