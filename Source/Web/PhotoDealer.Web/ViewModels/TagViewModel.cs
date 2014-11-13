namespace PhotoDealer.Web.ViewModels
{
    using AutoMapper;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>, IHaveCustomMappings
    {
        public int TagId { get; set; }

        public string Content { get; set; }

        public int Count { get; set; }

        // mapping
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Tag, TagViewModel>()
                .ForMember(m => m.Count, opt => opt.MapFrom(u => u.Pictures.Count));
        }
    }
}