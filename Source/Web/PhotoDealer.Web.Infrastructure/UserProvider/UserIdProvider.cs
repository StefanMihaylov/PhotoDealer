namespace PhotoDealer.Web.Infrastructure.UserProvider
{
    using System.Threading;
    using Microsoft.AspNet.Identity;

    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}
