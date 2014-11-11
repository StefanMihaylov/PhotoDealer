namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using PhotoDealer.Data;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Microsoft.AspNet.Identity.EntityFramework;
    using PhotoDealer.Web.Areas.Administration.ViewModels;

    public class UserController : AdminController
    {
        public UserController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        // GET: Administration/User
        public ActionResult Index()
        {
            var users = this.PhotoDb.Users.All().Project().To<UserViewModel>().First();

            return View(users);
        }

        public ActionResult GetRoles()
        {
            var roles = ((AppDbContext)this.PhotoDb.Context).Roles
                .OrderBy(x => x.Name)
                .Project().To<RoleViewModel>();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(UserViewModel updateUser)
        {
            if (ModelState.IsValid)
            {
                var user = this.PhotoDb.Users.GetById(updateUser.Id);

                if (user.Roles.Count == 0)
                {
                    user.Roles.Add(new IdentityUserRole() { RoleId = updateUser.RoleId });
                    this.PhotoDb.SaveChanges();
                }
                else
                {

                }
                
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}