namespace BillableHoursWebApp.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using DataTransferModels;
    using DataTransferModels.Project;
    using Microsoft.AspNet.Identity;
    using PubNubMessaging.Core;
    using Constants = Common.Constants;

    [EnableCors("*", "*", "*")]
    public class ProjectsController : ApiController
    {
        private IBillableHoursWebAppData data;
        private Pubnub pubnubClient;

        public ProjectsController(IBillableHoursWebAppData data)
        {
            this.data = data;
            pubnubClient = new Pubnub(Constants.PubnubPublishKey, Constants.PubnubSubscribeKey);
        }

        public ProjectsController()
            : this(new BillableHoursWebAppData())
        {
        }

        public IHttpActionResult Get()
        {
            var result = this.data.Projects
                .All()
                .ProjectTo<ProjectResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.data.Projects
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            var resultModel = Mapper.Map<ProjectResponseModel>(result);

            return this.Ok(resultModel);
        }

        [Authorize]
        [EnableCors("*", "*", "*")]
        [Route("~/api/users/projects")]
        [HttpGet]
        public IHttpActionResult GetUserProjects()
        {
            var currentUserId = User.Identity.GetUserId();

            Client client = this.data.Clients.Find(x => x.Id == currentUserId).FirstOrDefault();
            Employee employee;

            if (client == null)
            {
                var result = this.data.Employees
                    .Find(x => x.Id == currentUserId)
                    .SelectMany(x => x.Projects)
                    .ProjectTo<ProjectResponseModel>()
                    .ToList();

                return this.Ok(result);
            }
            else
            {
                var result = this.data.Clients
                    .Find(x => x.Id == currentUserId)
                    .SelectMany(x => x.Projects)
                    .ProjectTo<ProjectResponseModel>()
                    .ToList();

                return this.Ok(result);
            }
        }

        [EnableCors("*", "*", "*")]
        [Route("~/api/projects/category/{id}")]
        [HttpGet]
        public IHttpActionResult GetByCategory(int id)
        {
            var result = this.data.Projects
                .Find(x => x.CategoryId == id && !x.IsComplete)
                .ProjectTo<ProjectResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post([FromBody] ProjectRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserId = User.Identity.GetUserId();

            var user = this.data.Clients.Find(x => x.Id == currentUserId).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("Only clients can post projects!");
            }

            var projectToAdd = Mapper.Map<Project>(model);

            projectToAdd.Client = user;

            projectToAdd.TimePublished = DateTime.Now;

            this.data.Projects.Add(projectToAdd);
            this.data.SaveChanges();

            var message = string.Format("Post activity: User {0} created project {1} | {2}", user.Email, projectToAdd.Name, "/projects");
            pubnubClient.Publish<string>(channel: Constants.PubnubChannelActivityFeed, message: message, errorCallback:
                str => { }, userCallback:
                s => { });

            return this.Ok(projectToAdd.Id);
        }

        [Authorize]
        [Route("~/api/projects/complete/{id}")]
        [HttpPut]
        public IHttpActionResult FinalizeProject(int id)
        {
            var result = this.data.Projects
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            result.IsComplete = true;
            result.TimeFinished = DateTime.Now;

            var invoice = new Invoice
            {
                ProjectId = result.Id,
                ProjectTitle = result.Name,
                IssuedOn = DateTime.Now,
                EmployeeEmail = result.Employee.Email,
                EmployeeName = result.Employee.FirstName + " " + result.Employee.LastName,
                ClientEmail = result.Client.Email,
                ClientName = result.Client.FirstName + " " + result.Client.LastName,
                PricePerHour = result.PricePerHour,
                CategoryName = result.Category.Name,
                WorkLogs = result.WorkLogs
            };

            result.Client.Invoices.Add(invoice);

            data.Projects.Update(result);
            data.SaveChanges();

            var message = string.Format("Project activity: User {0} finalized {1}'s project | {2}", invoice.EmployeeEmail, invoice.ClientEmail, "/projects");
            pubnubClient.Publish<string>(channel: Constants.PubnubChannelActivityFeed, message: message, errorCallback:
                str => { }, userCallback:
                s => { });

            return this.Ok();
        }

        [Authorize]
        [Route("~/api/projects/complete/{id}")]
        [HttpGet]
        public IHttpActionResult GetInvoiceFromFinalizedProject(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var project = this.data.Projects.Find(x => x.Id == id).FirstOrDefault();

            if (project == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            if (!project.IsComplete)
            {
                return this.BadRequest("Project is not finished yet.");
            }

            if (project.ClientId != currentUserId)
            {
                return this.BadRequest("You are not authorized to meddle in other people's business... begone!");
            }

            var result = project.Client.Invoices.FirstOrDefault(x => x.ProjectId == project.Id);

            if (result == null)
            {
                return this.BadRequest("No invoice for this project.");
            }

            var mappedResult = Mapper.Map<InvoiceResponseModel>(result);

            return this.Ok(mappedResult);
        }

        [Authorize]
        [EnableCors("*", "*", "*")]
        [Route("~/api/projects/session/{id}")]
        [HttpPost]
        public IHttpActionResult BeginWorkLogSession(int id, [FromBody] ProjectWorkLogRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = this.data.Projects
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            var currentUserId = User.Identity.GetUserId();

            var user = this.data.Employees.Find(x => x.Id == currentUserId).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("Only employees can work on projects!");
            }

            var workLog = Mapper.Map<WorkLog>(model);
            workLog.StartTime = DateTime.Now;

            result.WorkLogs.Add(workLog);
            data.Projects.Update(result);
            data.SaveChanges();

            var message = string.Format("Project session activity: User {0} began session at {1} on {2}'s project | {3}", user.Email, workLog.StartTime, result.Client.Email, "/projects");
            pubnubClient.Publish<string>(channel: Constants.PubnubChannelActivityFeed, message: message, errorCallback:
                str => { }, userCallback:
                s => { });

            return this.Ok(workLog.Id);
        }

        [Authorize]
        [EnableCors("*", "*", "*")]
        [Route("~/api/projects/session/{id}")]
        [HttpPut]
        public IHttpActionResult EndWorkLogSession(int id)
        {
            var result = this.data.WorkLogs
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No worklog with that Id is active.");
            }

            if (result.EndTime != null)
            {
                return this.BadRequest("You cannot edit a recorded session!");
            }

            result.EndTime = DateTime.Now;
            data.WorkLogs.Update(result);
            data.SaveChanges();

            var message = string.Format("Project session activity: A user finished session at {0} | {1}", result.EndTime, "/projects");
            pubnubClient.Publish<string>(channel: Constants.PubnubChannelActivityFeed, message: message, errorCallback:
                str => { }, userCallback:
                s => { });

            return this.Ok();
        }

        [Authorize]
        public IHttpActionResult Put(int id)
        {
            var result = this.data.Projects
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            var currentUserId = User.Identity.GetUserId();

            var user = this.data.Employees.Find(x => x.Id == currentUserId).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("Only employees can work on projects!");
            }

            result.Employee = user;
            result.Employee.Projects.Add(result);

            this.data.Projects.Update(result);
            this.data.SaveChanges();

            var message = string.Format("Project activity: User {0} began working on {1}'s project | {2}", user.Email, result.Client.Email, "/projects");
            pubnubClient.Publish<string>(channel: Constants.PubnubChannelActivityFeed, message: message, errorCallback:
                str => { }, userCallback:
                s => { });

            return this.Ok(result.Id);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var result = this.data.Projects
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No category with that id is present.");
            }

            this.data.Projects.Delete(result);
            this.data.SaveChanges();

            var resultModel = Mapper.Map<ProjectResponseModel>(result);

            return this.Ok(resultModel);
        }
    }
}
