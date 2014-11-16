namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Logic;
    using PhotoDealer.Web.Areas.Administration.ViewModels;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    public class PictureController : AdminController
    {
        public const int NumberOfPictures = 6;
        private readonly IImageProcess imageProcess;

        public PictureController(IPhotoDealerData photoDb, IUserIdProvider userProvider, IImageProcess imageProcess)
            : base(photoDb, userProvider)
        {
            this.imageProcess = imageProcess;
        }

        // GET: Administration/Picture
        public ActionResult Index()
        {
            var pictures = this.GetPictures(NumberOfPictures);
            return this.View(pictures);
        }

        public ActionResult GetSmallImage(string id)
        {
            int width = 400;
            int quallity = 80;

            return this.GetPictureContent(id, width, quallity);
        }

        public ActionResult Approve(string id)
        {
            if (this.HttpContext.Request.IsAjaxRequest())
            {
                var picture = this.PhotoDb.Pictures.All().FirstOrDefault(p => p.PictureId.ToString() == id);
                if (picture == null)
                {
                    return this.HttpNotFound();
                }

                picture.IsVisible = true;
                this.PhotoDb.SaveChanges();

                var pictures = this.GetPictures(NumberOfPictures);
                return this.PartialView("_ApprovePicturePartial", pictures);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Delete(string id)
        {
            if (this.HttpContext.Request.IsAjaxRequest())
            {
                var picture = this.PhotoDb.Pictures.All().FirstOrDefault(p => p.PictureId.ToString() == id);
                if (picture == null)
                {
                    return this.HttpNotFound();
                }

                this.PhotoDb.Pictures.Delete(picture);
                this.PhotoDb.SaveChanges();

                var pictures = this.GetPictures(NumberOfPictures);
                return this.PartialView("_ApprovePicturePartial", pictures);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        private ActionResult GetPictureContent(string id, int width, int quallity)
        {
            var picture = this.PhotoDb.Pictures.All()
                              .Where(p => p.PictureId.ToString() == id)
                              .FirstOrDefault();

            if (picture == null)
            {
                return this.HttpNotFound();
            }
            else
            {
                MemoryStream outStream = this.imageProcess.Resize(picture.FileContent, width, quallity, false);
                outStream.Seek(0, SeekOrigin.Begin);
                return this.File(outStream, picture.FileContentType);
            }
        }

        private IEnumerable<PictureViewModel> GetPictures(int numberOfPictures)
        {
            return this.PhotoDb.Pictures.All().Where(p => p.IsVisible == false && p.IsPrivate == false)
                .Project().To<PictureViewModel>().OrderBy(p => p.CreatedOn).Take(numberOfPictures).ToList();
        }
    }
}