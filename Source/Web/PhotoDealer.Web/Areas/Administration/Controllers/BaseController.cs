using PhotoDealer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    public abstract class BaseController : Controller
    {
        private IPhotoDealerData photoDb;

        public BaseController(IPhotoDealerData photoDb)
        {
            this.PhotoDb = photoDb;
        }

        protected IPhotoDealerData PhotoDb
        {
            get { return this.photoDb; }
            set { this.photoDb = value; }
        }
    }
}