namespace PhotoDealer.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;

    public class CategoryController : BaseController
    {
        public CategoryController(IPhotoDealerData photoDb, UserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Index()
        {
            var categories = this.PhotoDb.Categories.All().Project().To<CategoryViewModel>();
            return this.View(categories);
        }

        [OutputCache(Duration = 15 * 60, VaryByParam = "groupId")]
        public JsonResult GetAll(int groupId)
        {
            var categories = this.PhotoDb.Categories.All()
                .Where(c => c.CategoryGroupId == groupId)
                .Project().To<CategoryViewModel>();
            return this.Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}