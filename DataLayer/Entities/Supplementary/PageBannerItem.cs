using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Supplementary
{
    public class PageBannerItem
    {
        public PageBannerItem()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name="عنوان")]
        public string? Title { get; set; }        
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name="متن 1")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Text1 { get; set; }        
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "متن 2")]
        public string? Text2 { get; set; }        
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "متن 3")]
        public string? Text3 { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "متن لینک")]
        public string? UrlText { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name="لینک")]
        public string? UrlLink { get; set; }
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name="تصویر")]
        public string? Image { get; set; }
        [Display(Name = "اولویت نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ViewOrder { get; set; }
        [Display(Name = "کد تخفیف دارد؟")]
        public bool HasDiscount { get; set; }
        [Display(Name="بسته بنر")]
        public int? PBId { get; set; }
        #region Relatoions
        [ForeignKey(nameof(PBId))]
        [Display(Name="بسته بنر")]
        public PageBanner? PageBanner { get; set; }
        #endregion
    }
}
