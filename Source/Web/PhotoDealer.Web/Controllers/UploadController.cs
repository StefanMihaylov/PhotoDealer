namespace PhotoDealer.Web.Controllers
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Web;
    using System.Web.Mvc;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Logic;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;

    [Authorize]
    public class UploadController : BaseController
    {
        private readonly IImageProcess imageProcess;

        public UploadController(IPhotoDealerData photoDb, IUserIdProvider userProvider, IImageProcess imageProcess)
            : base(photoDb, userProvider)
        {
            this.imageProcess = imageProcess;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase file, PictureViewModel inputData)
        {
            if (file != null && file.ContentLength > 0 && ModelState.IsValid)
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
                        FileContent = byteContent,
                        FileContentType = contentType,
                        FileName = fileName,
                        WidthPixels = image.Width,
                        HeightPixels = image.Height,

                        Price = NormalizePrice(inputData.Price),
                        Title = inputData.Title,
                        AuthorId = this.CurrentUserId,
                        OwnerId = this.CurrentUserId
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

        private decimal NormalizePrice(decimal input)
        {
            return Convert.ToInt32(input * 100m) / 100m;
        }
    }
}