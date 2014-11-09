using PhotoDealer.Data;
using PhotoDealer.Data.Models;
using PhotoDealer.Web.Infrastructure.UserProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private IPhotoDealerData photoDb;
        private string currentUserId;

        public BaseController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
        {
            this.PhotoDb = photoDb;
            this.CurrentUserId = userProvider.GetUserId();
        }

        protected IPhotoDealerData PhotoDb
        {
            get { return this.photoDb; }
            set { this.photoDb = value; }
        }

        protected string CurrentUserId
        {
            get { return this.currentUserId; }
            private set { this.currentUserId = value; }
        }
    }
}