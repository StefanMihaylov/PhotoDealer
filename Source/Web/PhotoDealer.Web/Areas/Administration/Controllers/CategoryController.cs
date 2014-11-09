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
using System.Web.Script.Serialization;
using PhotoDealer.Web.Infrastructure.UserProvider;


namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    public class CategoryController : BaseController
    {

        public CategoryController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Read(int categoryGroupId)
        {
            var categories = this.PhotoDb.Categories.All()
                .Where(c => c.CategoryGroupId == categoryGroupId)
                .Project().To<CategoryViewModel>().ToList();

            JsonpResult result = new JsonpResult(categories);
            return result;
        }

        public ActionResult Create(int categoryGroupId, [DataSourceRequest] DataSourceRequest request,
            CategoryViewModel newCategory)
        {
            if (newCategory != null && ModelState.IsValid)
            {
                var category = this.PhotoDb.Categories.All()
                    .Where(c => c.Name == newCategory.Name).FirstOrDefault();

                if (category == null)
                {
                    category = new Category();
                    this.PhotoDb.Categories.Add(category);
                }

                category.CategoryGroupId = categoryGroupId;
                category.Name = newCategory.Name;
                this.PhotoDb.SaveChanges();
            }

            return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(int categoryGroupId, [DataSourceRequest] DataSourceRequest request,
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
        public ActionResult Destroy(int categoryGroupId, [DataSourceRequest] DataSourceRequest request,
            CategoryViewModel newCategory)
        {
            if (newCategory != null)
            {
                var category = this.PhotoDb.Categories.GetById(newCategory.CategoryGroupId);

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