﻿namespace PhotoDealer.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        public string Username { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Credits { get; set; }

        [DisplayName("Registered on")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Rank")]
        [UIHint("RolesDropDown")]
        public string RoleId { get; set; }

        [DisplayName("Uploaded Pictures")]
        public int? AuthorPicturesCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(m => m.RoleId, opt => opt.MapFrom(u => u.Roles.FirstOrDefault().RoleId))
                .ForMember(m => m.AuthorPicturesCount, opt => opt.MapFrom(u => u.AuthorPictures.Where(p => p.IsDeleted == false && p.IsPrivate == false).Count()));
        }
    }
}