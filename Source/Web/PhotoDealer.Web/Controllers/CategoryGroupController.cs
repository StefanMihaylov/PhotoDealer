using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;
using PhotoDealer.Data;
using PhotoDealer.Web.ViewModels;

namespace PhotoDealer.Web.Controllers
{
    public class CategoryGroupController : BaseController
    {

        public CategoryGroupController(IPhotoDealerData photoDb)
            : base(photoDb)
        {
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = this.PhotoDb.CategoryGroups.All().Project().To<CategoryGroupViewModel>();
            return View(categories);
        }

        public JsonResult GetAll()
        {
            var categories = this.PhotoDb.CategoryGroups.All().Project().To<CategoryGroupViewModel>();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}