namespace PhotoDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
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
            return View();
        }

        //[OutputCache(Duration = 5 * 60)]
        public ActionResult GetTopPictures()
        {
            var topPictures = this.GetData().OrderByDescending(p => p.Downloads)
                                .Project().To<SmallPictureViewModel>().Take(6);
            return PartialView("_TopPhotos", topPictures.ToList());
        }

        //[OutputCache(Duration = 5 * 60)]
        public ActionResult GetLatestPictures()
        {
            var latestPictures = this.GetData().OrderByDescending(p => p.CreatedOn)
                                .Project().To<SmallPictureViewModel>().Take(6);
            return PartialView("_LatestPhotos", latestPictures.ToList());
        }

        private IQueryable<Picture> GetData()
        {
            return this.PhotoDb.Pictures.All();
        }

    }
}