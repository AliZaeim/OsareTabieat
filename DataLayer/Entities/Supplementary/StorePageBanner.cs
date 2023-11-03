using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Supplementary
{
    public class StorePageBanner
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="تصویر")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Image { get; set; }
        [Display(Name ="متن اول")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text1 { get; set; }
        [Display(Name ="متن دوم")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text2 { get; set; }
        [Display(Name ="متن سوم")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text3 { get; set; }
        [Display(Name ="آدرس پیوند")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? LinkUrl { get; set; }
        [Display(Name ="متن نمایشی پیوند")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? LinkText { get; set; }
        [Display(Name ="فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime? CreatedDate { get; set; }

    }
}
