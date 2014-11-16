namespace PhotoDealer.Web.Controllers
{
    using System.Web.Mvc;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Infrastructure.UserProvider;

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