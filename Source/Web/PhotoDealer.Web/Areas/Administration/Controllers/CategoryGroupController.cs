namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Common.Repository;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;

    public class CategoryGroupController : KendoBaseController<CategoryGroup, CategoryGroupViewModel>
    {
        public CategoryGroupController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        protected override IRepository<CategoryGroup> GetData()
        {
            return this.PhotoDb.CategoryGroups;
        }

        protected override object ModelId(CategoryGroupViewModel model)
        {
            return model.CategoryGroupId;
        }

        protected override void ReverseMapping(CategoryGroup databaseModel, CategoryGroupViewModel newModel)
        {
            databaseModel.GroupName = newModel.GroupName;
        }

        protected override Expression<Func<CategoryGroup, bool>> CheckIsUnique(CategoryGroupViewModel newModel)
        {
            return c => c.GroupName == newModel.GroupName;
        }
    }
}