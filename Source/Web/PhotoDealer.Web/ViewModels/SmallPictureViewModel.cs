namespace PhotoDealer.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class SmallPictureViewModel : BaseViewModel, IMapFrom<Picture>, IHaveCustomMappings
    {
        [UIHint("SmallPicture")]
        public string PictureId { get; set; }

        public int Downloads { get; set; }

        // mapping
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Picture, SmallPictureViewModel>()
                .ForMember(m => m.PictureId, opt => opt.MapFrom(u => u.PictureId.ToString()));
        }
    }
}