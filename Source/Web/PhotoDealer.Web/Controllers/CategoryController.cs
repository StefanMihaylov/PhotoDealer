using PhotoDealer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;
using PhotoDealer.Web.ViewModels.CategoryGroup;

namespace PhotoDealer.Web.Controllers
{
    public class CategoryController : BaseController
    {

        public CategoryController(IPhotoDealerData photoDb)
            : base(photoDb)
        {
        }


        // GET: Category
        public ActionResult Index()
        {
            var categories = this.PhotoDb.CategoryGroups.All().Project().To<CategoryGroupViewModel>();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string categoryGroup)
        {
            if (ModelState.IsValid)
            {

            }

            return null;
        }
    }
}