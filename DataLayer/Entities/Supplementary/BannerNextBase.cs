using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Supplementary
{
    public class BannerNextBase
    {
        public BannerNextBase()
        {
            BannerNextSliders = new List<BannerNextSlider>();
        }
        [Key]
        public int BannerNextBase_Id { get; set; }
        [Display(Name ="عنوان")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? BannerNextBase_Title { get; set; }
        [Display(Name ="نوع نمایش")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string? BannerNextBase_ShowingType { get; set; }
        [Display(Name ="فعال/غیرفعال")]
        public bool BannerNextBase_IsActive { get; set; }
        #region Relations
        public ICollection<BannerNextSlider> BannerNextSliders { get; set; }
        #endregion
    }
}
