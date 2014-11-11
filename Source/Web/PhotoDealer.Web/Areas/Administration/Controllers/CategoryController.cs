namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public class CategoryController : AdminController
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

           // JsonpResult result = new JsonpResult(categories);
            return HttpNotFound();
        }

        //public ActionResult Create(int categoryGroupId, [DataSourceRequest] DataSourceRequest request,
        //    CategoryViewModel newCategory)
        //{
        //    if (newCategory != null && ModelState.IsValid)
        //    {
        //        var category = this.PhotoDb.Categories.All()
        //            .Where(c => c.Name == newCategory.Name).FirstOrDefault();

        //        if (category == null)
        //        {
        //            category = new Category();
        //            this.PhotoDb.Categories.Add(category);
        //        }

        //        category.CategoryGroupId = categoryGroupId;
        //        category.Name = newCategory.Name;
        //        this.PhotoDb.SaveChanges();
        //    }

        //    return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Update(int categoryGroupId, [DataSourceRequest] DataSourceRequest request,
        //    CategoryViewModel newCategory)
        //{
        //    if (newCategory != null && ModelState.IsValid)
        //    {
        //        var category = this.PhotoDb.Categories.GetById(newCategory.CategoryId);

        //        if (category != null)
        //        {
        //            category.Name = newCategory.Name;
        //            this.PhotoDb.SaveChanges();
        //        }
        //    }

        //    return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Destroy(int categoryGroupId, [DataSourceRequest] DataSourceRequest request,
        //    CategoryViewModel newCategory)
        //{
        //    if (newCategory != null)
        //    {
        //        var category = this.PhotoDb.Categories.GetById(newCategory.CategoryGroupId);

        //        if (category != null)
        //        {
        //            this.PhotoDb.Categories.Delete(category);
        //            this.PhotoDb.SaveChanges();
        //        }
        //    }

        //    return Json(new[] { newCategory }.ToDataSourceResult(request, ModelState));
        //}
    }

    //public class JsonpResult : JsonResult
    //{
    //    object data = null;

    //    public JsonpResult()
    //    {
    //    }

    //    public JsonpResult(object data)
    //    {
    //        this.data = data;
    //    }

    //    public override void ExecuteResult(ControllerContext controllerContext)
    //    {
    //        if (controllerContext != null)
    //        {
    //            HttpResponseBase Response = controllerContext.HttpContext.Response;
    //            HttpRequestBase Request = controllerContext.HttpContext.Request;

    //            string callbackfunction = Request["callback"];
    //            if (string.IsNullOrEmpty(callbackfunction))
    //            {
    //                throw new Exception("Callback function name must be provided in the request!");
    //            }
    //            Response.ContentType = "application/x-javascript";
    //            if (data != null)
    //            {
    //                JavaScriptSerializer serializer = new JavaScriptSerializer();
    //                Response.Write(string.Format("{0}({1});", callbackfunction, serializer.Serialize(data)));
    //            }
    //        }
    //    }
    //}

}