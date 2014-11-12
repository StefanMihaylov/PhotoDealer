using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;
using PhotoDealer.Data;
using PhotoDealer.Web.ViewModels;
using PhotoDealer.Web.Infrastructure.UserProvider;

namespace PhotoDealer.Web.Controllers
{
    public class CategoryController : BaseController
    {

        public CategoryController(IPhotoDealerData photoDb, UserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }


        // GET: Category
        public ActionResult Index()
        {
            var categories = this.PhotoDb.Categories.All().Project().To<CategoryViewModel>();
            return View(categories);
        }

        [OutputCache(Duration = 15 * 60, VaryByParam = "groupId")]
        public JsonResult GetAll(int groupId)
        {
            var categories = this.PhotoDb.Categories.All()
                .Where(c => c.CategoryGroupId == groupId)
                .Project().To<CategoryViewModel>();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}