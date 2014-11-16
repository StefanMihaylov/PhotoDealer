namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Areas.Administration.ViewModels;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    public class RolesController : AdminController
    {
        public RolesController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        [OutputCache(Duration = 60 * 60)]
        public ActionResult GetRoles()
        {
            var roles = ((AppDbContext)this.PhotoDb.Context).Roles
                .OrderBy(x => x.Name)
                .Project().To<RoleViewModel>();
            return this.Json(roles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleName(string id)
        {
            var role = ((AppDbContext)this.PhotoDb.Context).Roles.Where(r => r.Id == id).FirstOrDefault();
            string result = string.Empty;
            if (role != null)
            {
                result = role.Name;
            }

            return this.Content(result);
        }
    }
}