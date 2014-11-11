namespace PhotoDealer.Web.Areas.Administration.ViewModels
{
    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        public string Username { get; set; }

        public decimal Credits { get; set; }

        public string RoleId { get; set; }

        public int? AuthorPicturesCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(m => m.RoleId, opt => opt.MapFrom(u => u.Roles.FirstOrDefault().RoleId));

            configuration.CreateMap<User, UserViewModel>()
                .ForMember(m => m.AuthorPicturesCount,
                        opt => opt.MapFrom(u => u.AuthorPictures
                            .Where(p => p.IsDeleted == false && p.IsPrivate == false).Count()));
        }
    }
}