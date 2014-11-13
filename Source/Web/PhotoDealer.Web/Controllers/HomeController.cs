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
        public const int NumberOfTopPictures = 9;

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
                                .Project().To<SmallPictureViewModel>().Take(NumberOfTopPictures);
            return PartialView("_TopPhotos", topPictures.ToList());
        }

        //[OutputCache(Duration = 5 * 60)]
        public ActionResult GetLatestPictures()
        {
            var latestPictures = this.GetData().OrderByDescending(p => p.CreatedOn)
                                .Project().To<SmallPictureViewModel>().Take(NumberOfTopPictures);
            return PartialView("_LatestPhotos", latestPictures.ToList());
        }

        //[OutputCache(Duration = 5 * 60)]
        public ActionResult GetTopTags()
        {
            var topTags = this.PhotoDb.Tags.All()
                .OrderByDescending(t => t.Pictures.Where(p => !p.IsDeleted && p.IsVisible && !p.IsPrivate).Count())
                .Project().To<TagViewModel>().Take(10);
            return PartialView("_TopTags", topTags.ToList());
        }

        private IQueryable<Picture> GetData()
        {
            return this.PhotoDb.Pictures.All().Where(p => p.IsVisible && !p.IsPrivate);
        }

    }
}