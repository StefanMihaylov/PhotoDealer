namespace PhotoDealer.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using PhotoDealer.Data;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    public class TransactionsController : AdminController
    {
        public TransactionsController(IPhotoDealerData photoDb, IUserIdProvider userProvider)
            : base(photoDb, userProvider)
        {
        }

        // GET: Administration/Transactions
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var transactions = this.PhotoDb.CreditTransactions.All()
                .Project().To<TransactionViewModel>();
            DataSourceResult result = transactions.ToDataSourceResult(request);
            return Json(result);
        }
    }
}