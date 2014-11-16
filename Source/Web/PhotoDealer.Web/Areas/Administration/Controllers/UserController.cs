﻿namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity.EntityFramework;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Areas.Administration.ViewModels;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    public class UserController : AdminController
    {
        public UserController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        // GET: Administration/User
        public ActionResult Index()
        {
            var users = this.PhotoDb.Users.All().Project().To<UserViewModel>();

            return this.View(users);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var users = this.PhotoDb.Users.All()
                .Project().To<UserViewModel>();
            DataSourceResult result = users.ToDataSourceResult(request);
            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, UserViewModel newUser)
        {
            if (newUser != null && this.ModelState.IsValid)
            {
                var user = this.PhotoDb.Users.GetById(newUser.Id);

                if (user != null)
                {
                    if (user.Roles.Count == 0 || this.CurrentUserId != user.Id)
                    {
                        if (user.Roles.Count > 0)
                        {
                            user.Roles.Clear();
                        }

                        if (newUser.RoleId != null)
                        {
                            user.Roles.Add(new IdentityUserRole() { RoleId = newUser.RoleId });
                        }
                    }
                    else
                    {
                        newUser.RoleId = user.Roles.First().RoleId;

                        // ModelState.AddModelError(string.Empty, "You cannotr change your Administaror rank!");
                    }

                    if (user.Credits != newUser.Credits)
                    {
                        var transaction = new CreditTransaction()
                        {
                            Amount = newUser.Credits - user.Credits,
                            BuyerId = this.CurrentUserId,
                            SellerId = user.Id,
                            PictureId = null
                        };

                        this.PhotoDb.CreditTransactions.Add(transaction);
                        user.Credits = newUser.Credits;
                    }

                    this.PhotoDb.SaveChanges();
                }
            }

            return this.Json(new[] { newUser }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, UserViewModel newUser)
        {
            if (newUser != null)
            {
                var user = this.PhotoDb.Users.GetById(newUser.Id);

                if (user != null && this.CurrentUserId != user.Id)
                {
                    this.PhotoDb.Users.Delete(user);
                    this.PhotoDb.SaveChanges();
                }
            }

            return this.Json(new[] { newUser }.ToDataSourceResult(request, this.ModelState));
        }
    }
}