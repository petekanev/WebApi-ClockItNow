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
    using DataTransferModels.Project;

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
                .GetById(id);

            if (result == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            var resultModel = Mapper.Map<ProjectResponseModel>(result);

            return this.Ok(resultModel);
        }

        [Authorize]
        public IHttpActionResult Post([FromBody] ProjectRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            /*
            var currentUser = Membership.GetUser(User.Identity.Name);
            string username = currentUser.UserName; //** get UserName
            var userId = currentUser.ProviderUserKey.ToString(); //** get user ID

            model.ClientId = userId;
            */

            var projectToAdd = Mapper.Map<Project>(model);

            projectToAdd.TimePublished = DateTime.Now;

            this.data.Projects.Add(projectToAdd);
            this.data.SaveChanges();

            return this.Ok(projectToAdd.Id);
        }

        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] ProjectRequestModel model)
        {
            var result = this.data.Projects
                .GetById(id);

            if (result == null)
            {
                return this.BadRequest("No project with that id is present.");
            }

            if (model.IsComplete)
            {
                result.IsComplete = true;

                this.data.Projects.Update(result);
                this.data.SaveChanges();

                return this.Ok(result);
            }

            var mappedAttachments = Mapper.Map<ICollection<Attachment>>(model.Attachments);

            result.CategoryId = model.CategoryId;
            result.Attachments = mappedAttachments;
            result.Description = model.Description;
            result.IsComplete = model.IsComplete;
            result.PricePerHour = model.PricePerHour;

            this.data.Projects.Update(result);
            this.data.SaveChanges();

            return this.Ok(result.Id);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var result = this.data.Projects
                               .GetById(id);

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
