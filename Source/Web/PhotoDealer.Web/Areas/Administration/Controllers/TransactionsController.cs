namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using PhotoDealer.Data;
    using PhotoDealer.Data.Common.Repository;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Areas.Administration.ViewModels;
    using PhotoDealer.Web.Infrastructure.UserProvider;

    public class TransactionsController : KendoBaseController<CreditTransaction, TransactionViewModel>
    {
        public TransactionsController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IRepository<CreditTransaction> GetData()
        {
            return this.PhotoDb.CreditTransactions;
        }

        protected override object ModelId(TransactionViewModel model)
        {
            return model.Id;
        }

        protected override void ReverseMapping(CreditTransaction dbModel, TransactionViewModel newModel)
        {
        }

        protected override Expression<Func<CreditTransaction, bool>> CheckIsUnique(TransactionViewModel newModel)
        {
            return c => false;
        }
    }
}