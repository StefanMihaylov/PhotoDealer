namespace PhotoDealer.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Logic;
    using PhotoDealer.Web.ViewModels;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.Infrastructure.Search;
    using System.Collections;
    using PhotoDealer.Data.Models;
    using System.Collections.Generic;

    public class PictureController : BaseController
    {
        public const int PageSize = 8;
        private readonly IImageProcess imageProcess;

        public PictureController(IPhotoDealerData photoDb, IUserIdProvider userProvider, IImageProcess imageProcess)
            : base(photoDb, userProvider)
        {
            this.imageProcess = imageProcess;
        }

        public ActionResult Index()
        {
            var pictures = this.PhotoDb.Pictures.All().Where(p => p.IsVisible == true && p.IsPrivate == false)
                .Project().To<SmallPictureViewModel>().ToList();
            return View(pictures);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PictureViewModel selected)
        {
            if (ModelState.IsValid)
            {
                return View(selected);
            }

            return View();
        }

        public ActionResult Details(string id)
        {
            var picture = this.PhotoDb.Pictures.All().Where(p => p.PictureId.ToString() == id)
                    .Project().To<PictureViewModel>().FirstOrDefault();

            if (picture == null)
            {
                return HttpNotFound();
            }

            return View(picture);
        }

        [OutputCache(Duration = 2 * 60, VaryByParam = "id")]
        public ActionResult GetSmallImage(string id)
        {
            int width = 300;
            int quallity = 60;

            return GetPictureContent(id, width, quallity);
        }

        [OutputCache(Duration = 2 * 60, VaryByParam = "id")]
        public ActionResult GetMediumImage(string id)
        {
            int width = 600;
            int quallity = 60;

            return GetPictureContent(id, width, quallity);
        }

        [HttpGet]
        public ActionResult Search(SearchViewModel search)
        {
           // this.SaveToSession("search", search);

            var filter = new FilterResults();
            var picturesQuery = this.PhotoDb.Pictures.All();

            picturesQuery = filter.FilterPictures(picturesQuery, search);
            var pictures = picturesQuery.Project().To<SmallPictureViewModel>();
            pictures = filter.Pagenation(pictures, search.Page, PageSize);

            return this.PartialView("_PicturesPagePartial", pictures.ToList());
        }

        private ActionResult GetPictureContent(string id, int width, int quallity)
        {
            var picture = this.PhotoDb.Pictures.All()
                              .Where(p => p.PictureId.ToString() == id && p.IsVisible)
                              .FirstOrDefault();

            if (picture == null)
            {
                return HttpNotFound();
            }
            else
            {
                MemoryStream outStream = this.imageProcess.Resize(picture.FileContent, width, quallity);
                outStream.Seek(0, SeekOrigin.Begin);
                return File(outStream, picture.FileContentType);
            }
        }

        private void SaveToSession<T>(string key, T value) where T : class
        {
            if (this.Session[key] == null)
            {
                this.Session[key] = value;
            }

            value = this.Session[key] as T;
        }
    }
}