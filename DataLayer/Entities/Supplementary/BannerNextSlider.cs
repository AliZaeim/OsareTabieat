using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Supplementary
{
    public class BannerNextSlider
    {
        public BannerNextSlider()
        {

        }
        [Key]
        public int Id { get; set; }
        [Display(Name ="نام بنــر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Title { get; set; }
        [Display(Name ="آدرس لینک")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Link { get; set; }
        [Display(Name ="تصویر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Image { get; set; }
        [Display(Name ="اولویت نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Priority { get; set; }
        [Display(Name ="فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name ="مجموعه")]
        public int? BannerNextBaseId { get; set; }
        #region Relations
        [ForeignKey(nameof(BannerNextBaseId))]
        public BannerNextBase? BannerNextBase { get; set; }
        #endregion

    }
}
