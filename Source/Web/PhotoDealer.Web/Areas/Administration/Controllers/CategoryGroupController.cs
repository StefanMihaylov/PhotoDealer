using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using PhotoDealer.Web.ViewModels;
using PhotoDealer.Data;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PhotoDealer.Data.Models;
using PhotoDealer.Web.Infrastructure.UserProvider;


namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    public class CategoryGroupController : AdminController
    {

        public CategoryGroupController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        // GET: Administration/CategoryGroup
        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Duration = 15 * 60)]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var categoryGroups = this.PhotoDb.CategoryGroups.All()
                .Project().To<CategoryGroupViewModel>();
            DataSourceResult result = categoryGroups.ToDataSourceResult(request);
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            CategoryGroupViewModel newCategoryGroup)
        {
            if (newCategoryGroup != null && ModelState.IsValid)
            {
                var categoryGroup = this.PhotoDb.CategoryGroups.All()
                    .Where(c => c.GroupName == newCategoryGroup.GroupName).FirstOrDefault();

                if (categoryGroup == null)
                {
                    categoryGroup = new CategoryGroup();
                    this.PhotoDb.CategoryGroups.Add(categoryGroup);
                }

                categoryGroup.GroupName = newCategoryGroup.GroupName;
                this.PhotoDb.SaveChanges();
            }

            return Json(new[] { newCategoryGroup }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            CategoryGroupViewModel newCategoryGroup)
        {
            if (newCategoryGroup != null && ModelState.IsValid)
            {
                var categoryGroup = this.PhotoDb.CategoryGroups.GetById(newCategoryGroup.CategoryGroupId);

                if (categoryGroup != null)
                {
                    categoryGroup.GroupName = newCategoryGroup.GroupName;
                    this.PhotoDb.SaveChanges();
                }
            }

            return Json(new[] { newCategoryGroup }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            CategoryGroupViewModel newCategoryGroup)
        {
            if (newCategoryGroup != null)
            {
                var categoryGroup = this.PhotoDb.CategoryGroups.GetById(newCategoryGroup.CategoryGroupId);

                if (categoryGroup != null)
                {
                    this.PhotoDb.CategoryGroups.Delete(categoryGroup);
                    this.PhotoDb.SaveChanges();
                }
            }

            return Json(new[] { newCategoryGroup }.ToDataSourceResult(request, ModelState));
        }
    }
}