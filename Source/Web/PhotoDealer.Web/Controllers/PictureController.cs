namespace PhotoDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Logic;

    using AutoMapper.QueryableExtensions;
    using PhotoDealer.Web.ViewModels;

    public class PictureController : BaseController
    {
        private readonly IImageProcess imageProcess;

        public PictureController(IPhotoDealerData photoDb, IImageProcess imageProcess)
            : base(photoDb)
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

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = file.FileName;
                string contentType = file.ContentType;
                int lenght = file.ContentLength;

                if (IsPicture(contentType))
                {
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    byte[] byteContent = reader.ReadBytes(lenght);

                    var image = Image.FromStream(file.InputStream);
                    var picture = new Picture()
                    {
                        Title = "Title",
                        FileContent = byteContent,
                        FileContentType = contentType,
                        FileName = fileName,
                        WidthPixels = image.Width,
                        HeightPixels = image.Height
                    };

                    this.PhotoDb.Pictures.Add(picture);
                    this.PhotoDb.SaveChanges();
                }
                else
                {
                    // wrong file format
                }
            }
            else
            {
                // no file
            }


            return View();
        }

        private bool IsPicture(string contentType)
        {
            var contentTypeParts = contentType.Split('/');
            return contentTypeParts[0] == "image";
        }
    }
}