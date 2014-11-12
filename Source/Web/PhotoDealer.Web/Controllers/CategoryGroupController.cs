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
            return View(categories);
        }

        [OutputCache(Duration = 15 * 60)]
        public JsonResult GetAll()
        {
            var categories = this.PhotoDb.CategoryGroups.All().Project().To<CategoryGroupViewModel>();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}