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
using System.Net;
using PhotoDealer.Data.Common.Repository;
using System.Linq.Expressions;


namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    public class SubCategoryController : AdminController
    {

        public SubCategoryController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Duration = 15 * 60, VaryByParam = "id")]
        public ActionResult Read(int? id, [DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result;

            if (id == null)
            {
                return Json(new DataSourceResult());
            }
            else
            {
                var categories = this.PhotoDb.Categories.All()
                    .Where(c => c.CategoryGroupId == id.Value)
                    .Project().To<CategoryViewModel>();
                result = categories.ToDataSourceResult(request);
            }

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(int? id, [DataSourceRequest] DataSourceRequest request,
            CategoryViewModel newCategory)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (newCategory != null && ModelState.IsValid)
            {
                var category = this.PhotoDb.Categories.All()
                    .Where(c => c.Name == newCategory.Name).FirstOrDefault();

                if (category == null)
                {
                    category = new Category();
                    category.CategoryGroupId = id.Value;
                    this.PhotoDb.Categories.Add(category);
                }

                category.Name = newCategory.Name;
                this.PhotoDb.SaveChanges();
            }

            return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            CategoryViewModel newCategory)
        {
            if (newCategory != null && ModelState.IsValid)
            {
                var category = this.PhotoDb.Categories.GetById(newCategory.CategoryId);

                if (category != null)
                {
                    category.Name = newCategory.Name;
                    this.PhotoDb.SaveChanges();
                }
            }

            return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request,
            CategoryViewModel newCategory)
        {
            if (newCategory != null)
            {
                var category = this.PhotoDb.Categories.GetById(newCategory.CategoryId);

                if (category != null)
                {
                    this.PhotoDb.Categories.Delete(category);
                    this.PhotoDb.SaveChanges();
                }
            }

            return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        }
    }
}