namespace PhotoDealer.Web.Controllers
{
    using PhotoDealer.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using PhotoDealer.Web.ViewModels.Category;

    public class HomeController : BaseController
    {

        public HomeController(IPhotoDealerData photoDb)
            : base(photoDb)
        {
        }

        public ActionResult Index()
        {
            var categories = this.PhotoDb.Categories.All().Project().To<CategoryViewModel>();
            return View(categories);
        }
    }
}