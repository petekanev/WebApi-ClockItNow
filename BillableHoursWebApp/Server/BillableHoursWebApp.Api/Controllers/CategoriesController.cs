namespace BillableHoursWebApp.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using DataTransferModels;

    [EnableCors("*", "*", "*")]
    public class CategoriesController : ApiController
    {
        private IBillableHoursWebAppData data;

        public CategoriesController(IBillableHoursWebAppData data)
        {
            this.data = data;
        }

        // Poor man's DI
        // TODO: Replace with Ninject
        public CategoriesController()
            : this(new BillableHoursWebAppData())
        {
        }

        public IHttpActionResult Get()
        {
            var result = this.data.Categories
                .All()
                .ProjectTo<CategoryResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = this.data.Categories
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No category with that id is present.");
            }

            var resultModel = Mapper.Map<CategoryResponseModel>(result);

            return this.Ok(resultModel);
        }

        [Authorize]
        public IHttpActionResult Post([FromBody] CategoryRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (this.data.Categories
                .Find(x => x.Name.ToLowerInvariant() == model.Name.ToLowerInvariant()).FirstOrDefault() != null)
            {
                return this.BadRequest("A category with that name exists already!");
            }

            var categoryToAdd = Mapper.Map<Category>(model);

            this.data.Categories.Add(categoryToAdd);
            this.data.SaveChanges();

            return this.Ok(categoryToAdd.Id);
        }

        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] CategoryRequestModel model)
        {
            var result = this.data.Categories
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No category with that id is present.");
            }

            if (this.data.Categories.Find(x => x.Name.ToLowerInvariant() == model.Name.ToLowerInvariant()) != null)
            {
                return this.BadRequest("A category with that name exists already!");
            }

            result.Name = model.Name;

            this.data.Categories.Update(result);
            this.data.SaveChanges();

            return this.Ok(result.Id);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var result = this.data.Categories
                .Find(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("No category with that id is present.");
            }

            this.data.Categories.Delete(result);
            this.data.SaveChanges();

            var resultModel = Mapper.Map<CategoryResponseModel>(result);

            return this.Ok(resultModel);
        }
    }
}