using PhotoDealer.Data;
using PhotoDealer.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var categories = this.PhotoDb.CategoryGroups.All()
                .Select(c => new CategoryDropDownViewModel { Id = c.CategoryGroupId, Name = c.GroupName })
                .ToList();

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