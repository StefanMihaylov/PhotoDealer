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
            var picture = this.PhotoDb.Pictures.All().Project().To<PictureViewModel>().First();
            return View(picture);
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

        public ActionResult GetImage()
        {
            var picture = this.PhotoDb.Pictures.All().First();

            MemoryStream outStream = this.imageProcess.Resize(picture.FileContent, 300, 60);
            outStream.Seek(0, SeekOrigin.Begin);
            return File(outStream, picture.FileContentType);
        }
    }
}