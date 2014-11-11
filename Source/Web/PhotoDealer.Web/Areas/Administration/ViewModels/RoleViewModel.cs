using Microsoft.AspNet.Identity.EntityFramework;
using PhotoDealer.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoDealer.Web.Areas.Administration.ViewModels
{
    public class RoleViewModel: IMapFrom<IdentityRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}