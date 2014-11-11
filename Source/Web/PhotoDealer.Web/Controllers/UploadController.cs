namespace PhotoDealer.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
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
                        CategoryGroupId = inputData.CategoryGroupId,
                        CategoryId = inputData.CategoryId,
                        AuthorId = this.CurrentUserId,
                        OwnerId = this.CurrentUserId,

                        IsVisible = false
                    };

                    // picture.IsVisible = true;
                    if (inputData.TagString != null)
                    {
                        AddNewTags(this.PhotoDb, inputData.TagString, picture);
                    }

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

            return RedirectToAction("Index", "Picture", null);
        }

        private void AddNewTags(IPhotoDealerData photoDealerData, string tagsString, Picture picture)
        {
            string[] tagTexts = tagsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tagText in tagTexts)
            {
                var newTagtext = tagText.Trim();
                var tag = photoDealerData.Tags.All().FirstOrDefault(t => t.Content == newTagtext);
                if (tag == null)
                {
                    tag = new Tag() { Content = newTagtext };
                    photoDealerData.Tags.Add(tag);
                }

                picture.Tags.Add(tag);
            }
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