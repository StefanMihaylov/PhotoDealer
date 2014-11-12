namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Controllers;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Common;

    //[Authorize]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }
    }
}