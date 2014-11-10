namespace PhotoDealer.Web.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseViewModel
    {
        [UIHint("SmallDate")]
        [DisplayName("Published on")]
        public DateTime? CreatedOn { get; set; }
    }
}