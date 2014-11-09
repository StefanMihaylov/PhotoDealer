namespace PhotoDealer.Web.Controllers
{
    using PhotoDealer.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using PhotoDealer.Web.ViewModels;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    public class HomeController : BaseController
    {

        public HomeController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Index()
        {
            var categories = this.PhotoDb.Categories.All().Project().To<CategoryViewModel>();
            return View(categories);
        }
    }
}