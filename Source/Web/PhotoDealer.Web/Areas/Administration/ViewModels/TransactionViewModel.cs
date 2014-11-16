namespace PhotoDealer.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Infrastructure.Mapping;

    public class TransactionViewModel : IMapFrom<CreditTransaction>, IHaveCustomMappings
    {
        [Key]
        public string Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [DisplayName("Picture Details")]
        public string PictureId { get; set; }

        public string Seller { get; set; }

        public string Buyer { get; set; }

        [DisplayName("Registered on")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime CreatedOn { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<CreditTransaction, TransactionViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(u => u.CreditTransactionId.ToString()))
                .ForMember(m => m.PictureId, opt => opt.MapFrom(u => u.PictureId.ToString()))
                .ForMember(m => m.Seller, opt => opt.MapFrom(u => u.Seller.UserName))
                .ForMember(m => m.Buyer, opt => opt.MapFrom(u => u.Buyer.UserName));
        }
    }
}