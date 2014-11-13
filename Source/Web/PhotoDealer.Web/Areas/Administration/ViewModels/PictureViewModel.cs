namespace PhotoDealer.Web.Areas.Administration.ViewModels
{
    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using PhotoDealer.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PictureViewModel : BaseViewModel, IMapFrom<Picture>, IHaveCustomMappings
    {
        [UIHint("AdminSmallPicture")]
        public string PictureId { get; set; }

        [DisplayName("Picture Title")]
        public string Title { get; set; }

        [UIHint("CategoryGroup")]
        public int CategoryGroupId { get; set; }

        [UIHint("Category")]
        public int? CategoryId { get; set; }

        public bool IsVisible { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Picture, PictureViewModel>()
                .ForMember(m => m.PictureId, opt => opt.MapFrom(u => u.PictureId.ToString()))
                .ForMember(m => m.Author, opt => opt.MapFrom(u => u.Author.UserName));
        }
    }
}