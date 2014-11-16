namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;

    public class SubCategoryController : AdminController
    {
        public SubCategoryController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        // [OutputCache(Duration = 15 * 60, VaryByParam = "id")]
        public ActionResult Read(int? id, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result;

            if (id == null)
            {
                return this.Json(new DataSourceResult());
            }
            else
            {
                var categories = this.PhotoDb.Categories.All()
                    .Where(c => c.CategoryGroupId == id.Value)
                    .Project().To<CategoryViewModel>();
                result = categories.ToDataSourceResult(request);
            }

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int? id, [DataSourceRequest] DataSourceRequest request, CategoryViewModel newCategory)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (newCategory != null && ModelState.IsValid)
            {
                var category = this.PhotoDb.Categories.All()
                    .Where(c => c.Name == newCategory.Name).FirstOrDefault();

                if (category == null)
                {
                    category = new Category();
                    category.CategoryGroupId = id.Value;
                    this.PhotoDb.Categories.Add(category);
                }

                category.Name = newCategory.Name;
                this.PhotoDb.SaveChanges();
            }

            return this.Json(new[] { newCategory }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, CategoryViewModel newCategory)
        {
            if (newCategory != null && ModelState.IsValid)
            {
                var category = this.PhotoDb.Categories.GetById(newCategory.CategoryId);

                if (category != null)
                {
                    category.Name = newCategory.Name;
                    this.PhotoDb.SaveChanges();
                }
            }

            return this.Json(new[] { newCategory }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, CategoryViewModel newCategory)
        {
            if (newCategory != null)
            {
                var category = this.PhotoDb.Categories.GetById(newCategory.CategoryId);

                if (category != null)
                {
                    this.PhotoDb.Categories.Delete(category);
                    this.PhotoDb.SaveChanges();
                }
            }

            return this.Json(new[] { newCategory }.ToDataSourceResult(request, this.ModelState));
        }
    }
}