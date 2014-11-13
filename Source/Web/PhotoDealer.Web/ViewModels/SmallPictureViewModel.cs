namespace PhotoDealer.Web.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;        

    public class SmallPictureViewModel : IMapFrom<Picture>, IHaveCustomMappings
    {
        [UIHint("SmallPicture")]
        public string PictureId { get; set; }

        public int Downloads { get; set; }

        [UIHint("SmallHour")]
        [DisplayName("Published on")]
        public DateTime? CreatedOn { get; set; }

        // mapping
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Picture, SmallPictureViewModel>()
                .ForMember(m => m.PictureId, opt => opt.MapFrom(u => u.PictureId.ToString()));
        }
    }
}