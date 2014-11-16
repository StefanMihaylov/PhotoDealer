namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Common.Models;
    using PhotoDealer.Data.Common.Repository;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    public abstract class KendoBaseController<T, Tmodel> : AdminController where T : class, IDeletableEntity
    {
        public KendoBaseController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        protected abstract IRepository<T> GetData();

        protected abstract object ModelId(Tmodel model);

        protected abstract void ReverseMapping(T dbModel, Tmodel newModel);

        protected abstract Expression<Func<T, bool>> CheckIsUnique(Tmodel newModel);


        public virtual ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var queryModel = this.GetData().All()
                                 .Project().To<Tmodel>();
            DataSourceResult result = queryModel.ToDataSourceResult(request);
            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create([DataSourceRequest] DataSourceRequest request, Tmodel newModel)
        {
            if (newModel != null && this.ModelState.IsValid)
            {
                var dbModel = this.GetData().All().Where(this.CheckIsUnique(newModel)).FirstOrDefault();
                if (dbModel == null)
                {
                    dbModel = (T)Activator.CreateInstance(typeof(T));
                    this.ReverseMapping(dbModel, newModel);
                    this.GetData().Add(dbModel);
                }
                else
                {
                    dbModel.IsDeleted = false;
                    dbModel.DeletedOn = null;
                }

                this.PhotoDb.SaveChanges();
            }

            return this.JsonKendoResult(newModel, request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Update([DataSourceRequest] DataSourceRequest request, Tmodel newModel)
        {
            if (newModel != null && this.ModelState.IsValid)
            {
                var dbModel = this.GetById(newModel);
                if (dbModel != null)
                {
                    this.ReverseMapping(dbModel, newModel);
                    this.PhotoDb.SaveChanges();
                }
            }

            return this.JsonKendoResult(newModel, request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Destroy([DataSourceRequest] DataSourceRequest request, Tmodel newModel)
        {
            if (newModel != null)
            {
                var dbModel = this.GetById(newModel);
                if (dbModel != null)
                {
                    this.GetData().Delete(dbModel);
                    this.PhotoDb.SaveChanges();
                }
            }

            return this.JsonKendoResult(newModel, request);
        }

        private ActionResult JsonKendoResult(Tmodel newModel, DataSourceRequest request)
        {
            return this.Json(new[] { newModel }.ToDataSourceResult(request, this.ModelState));
        }

        private T GetById(Tmodel newModel)
        {
            return this.GetData().GetById(this.ModelId(newModel));
        }
    }
}