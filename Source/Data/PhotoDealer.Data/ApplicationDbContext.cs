namespace PhotoDealer.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using PhotoDealer.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PhotoDealerDb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
