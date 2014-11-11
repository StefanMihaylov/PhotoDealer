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

    public class PictureController : BaseController
    {
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

        public ActionResult GetSmallImage(string id)
        {
            int width = 300;
            int quallity = 60;

            return GetPictureContent(id, width, quallity);
        }

        public ActionResult GetMediumImage(string id)
        {
            int width = 600;
            int quallity = 60;

            return GetPictureContent(id, width, quallity);
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
    }
}