namespace PhotoDealer.Web.Areas.Administration.ViewModels
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class RoleViewModel : IMapFrom<IdentityRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}