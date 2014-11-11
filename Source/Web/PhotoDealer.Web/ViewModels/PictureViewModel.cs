namespace PhotoDealer.Web.ViewModels
{
    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PictureViewModel : BaseViewModel, IMapFrom<Picture>, IHaveCustomMappings
    {
        [UIHint("MediumPicture")]
        public string PictureId { get; set; }

        [Required]
        [DisplayName("Picture Title")]
        public string Title { get; set; }

        [Required]
        [UIHint("CategoryGroup")]
        public int CategoryGroupId { get; set; }

        [DisplayName("Category Group")]
        public string CategoryGroup { get; set; }


        [UIHint("Category")]
        public int? CategoryId { get; set; }

        public string Category { get; set; }

        public int Downloads { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public bool IsVisible { get; set; }

        [DisplayName("Tags")]
        public string TagString { get; set; }

        [UIHint("JoinTags")]
        public ICollection<Tag> Tags { get; set; }


        public string AuthorId { get; set; }

        public virtual string Author { get; set; }

        public string OwnerId { get; set; }

        public virtual string Owner { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Picture, PictureViewModel>()
                .ForMember(m => m.PictureId, opt => opt.MapFrom(u => u.PictureId.ToString()));

            configuration.CreateMap<Picture, PictureViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(u => u.Author.UserName));

            configuration.CreateMap<Picture, PictureViewModel>()
                .ForMember(m => m.Owner, opt => opt.MapFrom(u => u.Owner.UserName));

            configuration.CreateMap<Picture, PictureViewModel>()
                .ForMember(m => m.CategoryGroup, opt => opt.MapFrom(u => u.CategoryGroup.GroupName));

            configuration.CreateMap<Picture, PictureViewModel>()
                .ForMember(m => m.Category, opt => opt.MapFrom(u => u.Category.Name));
        }
    }
}