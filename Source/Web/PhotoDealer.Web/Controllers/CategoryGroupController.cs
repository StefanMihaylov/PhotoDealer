namespace PhotoDealer.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;

    public class CategoryGroupController : BaseController
    {
        public CategoryGroupController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = this.PhotoDb.CategoryGroups.All().Project().To<CategoryGroupViewModel>();
            return this.View(categories);
        }

        [OutputCache(Duration = 15 * 60)]
        public JsonResult GetAll()
        {
            var categories = this.PhotoDb.CategoryGroups.All().Project().To<CategoryGroupViewModel>();
            return this.Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}