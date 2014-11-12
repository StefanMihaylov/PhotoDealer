using PhotoDealer.Data;
using PhotoDealer.Web.Infrastructure.UserProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using PhotoDealer.Web.Areas.Administration.ViewModels;
using PhotoDealer.Common;

namespace PhotoDealer.Web.Controllers
{
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

        protected bool IsAdmin(string userId)
        {
            var allRoles = ((AppDbContext)this.PhotoDb.Context).Roles.ToList();                

            var user = this.PhotoDb.Users.GetById(userId);
            if (user != null)
            {
                foreach (var role in user.Roles)
                {
                    var roleName = allRoles.FirstOrDefault(r => r.Id == role.RoleId);
                    if (roleName != null)
                    {
                        if (roleName.Name == GlobalConstants.AdministratorRoleName)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}