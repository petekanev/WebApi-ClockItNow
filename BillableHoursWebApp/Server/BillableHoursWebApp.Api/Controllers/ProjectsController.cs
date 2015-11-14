namespace BillableHoursWebApp.Api.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Security;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using DataTransferModels;
    using DataTransferModels.Project;
    using Microsoft.AspNet.Identity;

    [EnableCors("*", "*", "*")]
    public class ProjectsController : ApiController
    {
        private IBillableHoursWebAppData data;

        public ProjectsController(IBillableHoursWebAppData data)
        {
            this.data = data;
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

            return this.Ok(projectToAdd.Id);
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
