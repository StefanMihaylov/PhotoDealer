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

    public class PhotoController : BaseController
    {
        private readonly IImageProcess imageProcess;

        public PhotoController(IPhotoDealerData photoDb, IImageProcess imageProcess)
            : base(photoDb)
        {
            this.imageProcess = imageProcess;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImage()
        {
            var picture = this.PhotoDb.Pictures.All().First();

            using (MemoryStream inStream = new MemoryStream(picture.Content))
            {
                MemoryStream outStream = this.imageProcess.Resize(inStream, 300, 60);
                outStream.Seek(0, SeekOrigin.Begin);
                return File(outStream, picture.ContentType);
            }
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
                        Content = byteContent,
                        ContentType = contentType,
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