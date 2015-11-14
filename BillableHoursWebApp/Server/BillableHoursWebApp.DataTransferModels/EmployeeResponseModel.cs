namespace BillableHoursWebApp.DataTransferModels
{
    using System.Collections.Generic;
    using Common.Mapping;
    using Data.Models;
    using Project;

    public class EmployeeResponseModel : IMapFrom<Employee>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        // public IEnumerable<ProjectResponseModel> Projects { get; set; } 
    }
}
