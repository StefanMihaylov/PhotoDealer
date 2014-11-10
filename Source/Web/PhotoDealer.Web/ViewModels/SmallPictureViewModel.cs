
namespace PhotoDealer.Web.ViewModels
{
    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class SmallPictureViewModel : IMapFrom<Picture>, IHaveCustomMappings
    {
        [UIHint("SmallPicture")]
        public string PictureId { get; set; }

        // mapping
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Picture, SmallPictureViewModel>()
                .ForMember(m => m.PictureId, opt => opt.MapFrom(u => u.PictureId.ToString()));
        }
    }
}