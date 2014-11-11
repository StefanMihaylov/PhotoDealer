namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.Areas.Administration.ViewModels;
    using PhotoDealer.Logic;
    using System.IO;
    using System.Net;

    public class PictureController : AdminController
    {
        private readonly IImageProcess imageProcess;

        public PictureController(IPhotoDealerData photoDb, IUserIdProvider userProvider, IImageProcess imageProcess)
            : base(photoDb, userProvider)
        {
            this.imageProcess = imageProcess;
        }

        // GET: Administration/Picture
        public ActionResult Index()
        {
            var pictures = this.PhotoDb.Pictures.All().Where(p => p.IsVisible == false && p.IsPrivate == false)
                .Project().To<PictureViewModel>().ToList();
            return View(pictures);
        }

        public ActionResult GetSmallImage(string id)
        {
            int width = 400;
            int quallity = 80;

            return GetPictureContent(id, width, quallity);
        }

        [HttpPost]
        public ActionResult Approve(string id)
        {
            if (this.HttpContext.Request.IsAjaxRequest())
            {
                var picture = this.PhotoDb.Pictures.All().FirstOrDefault(p => p.PictureId.ToString() == id);
                if (picture == null)
                {
                    return HttpNotFound();
                }

                picture.IsVisible = true;
                this.PhotoDb.SaveChanges();

                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (this.HttpContext.Request.IsAjaxRequest())
            {
                var picture = this.PhotoDb.Pictures.All().FirstOrDefault(p => p.PictureId.ToString() == id);
                if (picture == null)
                {
                    return HttpNotFound();
                }

                this.PhotoDb.Pictures.Delete(picture);
                this.PhotoDb.SaveChanges();
                return RedirectToAction("Index");
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
                return HttpNotFound();
            }
            else
            {
                MemoryStream outStream = this.imageProcess.Resize(picture.FileContent, width, quallity, false);
                outStream.Seek(0, SeekOrigin.Begin);
                return File(outStream, picture.FileContentType);
            }
        }
    }
}