namespace PhotoDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using PhotoDealer.Common;
    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Logic;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.Extensions;
    using PhotoDealer.Web.ViewModels;


    public class DownloadController : BaseController
    {
        private readonly IImageProcess imageProcess;

        public DownloadController(IPhotoDealerData photoDb, IUserIdProvider userProvider, IImageProcess imageProcess)
            : base(photoDb, userProvider)
        {
            this.imageProcess = imageProcess;
        }

        public ActionResult Download(string pictureId, PictureSizeEnum? pictureSize)
        {
            if (pictureId == null || pictureSize == null)
            {
                return RedirectToIndex();
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            var user = this.PhotoDb.Users.GetById(this.CurrentUserId);
            var picture = GetPictureFromDb(pictureId);

            int width = 1;
            decimal price = 0m;
            switch (pictureSize)
            {
                case PictureSizeEnum.Small:
                    width = picture.WidthPixels / 10;
                    price = picture.Price / 10;
                    break;
                case PictureSizeEnum.Medium:
                    width = picture.WidthPixels / 2;
                    price = picture.Price / 2;
                    break;
                case PictureSizeEnum.Original:
                    width = picture.WidthPixels;
                    price = picture.Price / 1;
                    break;
                case PictureSizeEnum.BuyRights:
                    width = picture.WidthPixels;
                    price = picture.Price * 1000;
                    break;
            }

            if (user.Credits < price)
            {
                this.AddNotification("You don't have enough money!", NotificationType.ERROR);
                return RedirectToDetails(pictureId);
            }

            var author = this.PhotoDb.Users.GetById(picture.AuthorId);
            if (author == null)
            {
                this.AddNotification("Unknown picture author!", NotificationType.ERROR);
                return RedirectToDetails(pictureId);
            }

            author.Credits += price;
            user.Credits -= price;

            var transaction = new CreditTransaction()
            {
                Amount = price,
                BuyerId = this.CurrentUserId,
                SellerId = picture.AuthorId,
                PictureId = picture.PictureId,
            };

            this.PhotoDb.CreditTransactions.Add(transaction);

            picture.Downloads++;

            if (pictureSize == PictureSizeEnum.BuyRights)
            {
                picture.OwnerId = this.CurrentUserId;
                picture.IsPrivate = true;
            }

            this.PhotoDb.SaveChanges();

            return DownloadPictureContent(picture, width);
        }

        public ActionResult AuthorDownload(string id)
        {
            var picture = GetPictureFromDb(id);

            if (IsOwner(picture.AuthorId))
            {
                return DownloadOriginalPicture(picture);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult Edit(string id)
        {
            var picture = GetPictureFromDb(id);
            if (picture == null)
            {
                return HttpNotFound();
            }


            if (IsOwner(picture.AuthorId))
            {
                return RedirectToDetails(id);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Delete(string id)
        {
            var picture = GetPictureFromDb(id);
            if (picture == null)
            {
                return HttpNotFound();
            }

            if (IsOwner(picture.OwnerId))
            {
                this.PhotoDb.Pictures.Delete(picture);
                this.PhotoDb.SaveChanges();

                return RedirectToIndex();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        private ActionResult DownloadOriginalPicture(Picture picture)
        {
            if (picture == null)
            {
                return this.HttpNotFound();
            }

            return this.File(picture.FileContent, picture.FileContentType, picture.FileName);
        }

        private ActionResult DownloadPictureContent(Picture picture, int width, int quallity = 80)
        {
            if (picture == null)
            {
                return this.HttpNotFound();
            }
            else
            {
                MemoryStream outStream = this.imageProcess.Resize(picture.FileContent, width, quallity, false);
                outStream.Seek(0, SeekOrigin.Begin);
                return this.File(outStream, picture.FileContentType, picture.FileName);
            }
        }

        private Picture GetPictureFromDb(string id)
        {
            var picture = this.PhotoDb.Pictures.All()
                  .Where(p => p.PictureId.ToString() == id && p.IsVisible)
                  .FirstOrDefault();

            return picture;
        }

        private bool IsOwner(string authorId)
        {
            return (this.CurrentUserId == authorId || this.User.IsInRole(GlobalConstants.AdministratorRoleName));
        }

        private ActionResult RedirectToIndex()
        {
            return this.RedirectToAction("Index", "Picture", new { area = string.Empty });
        }

        private ActionResult RedirectToDetails(string id)
        {
            return this.RedirectToAction("Details", "Picture", new { area = string.Empty, id = id });
        }
    }
}