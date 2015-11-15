namespace BillableHoursWebApp.Api.Controllers
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using DataTransferModels.Project;
    using Microsoft.AspNet.Identity;

    [EnableCors("*", "*", "*")]
    public class WorkLogsController : ApiController
    {
        public IHttpActionResult Get()
        {
            return this.NotFound();
        }

        public IHttpActionResult Get(int id)
        {
            return this.NotFound();
        }

        [Authorize]
        public IHttpActionResult Post([FromBody] ProjectWorkLogRequestModel model)
        {
            return this.NotFound();
        }

        public IHttpActionResult Put()
        {
            return this.NotFound();
        }

        public IHttpActionResult Delete()
        {
            return this.NotFound();
        }
    }
}
