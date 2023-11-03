using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class PageBanner
    {
        public PageBanner()
        {
            PageBannerItems = new List<PageBannerItem>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name="عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Title { get; set; }
        
        [Display(Name="اولویت نمایش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ViewOrder { get; set; }
        [Display(Name = "تعداد بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? BannerCount { get; set; }
        
        [Display(Name="تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name="فعال/غیرفعال")]
        public bool IsActive { get; set; }
        #region Relations
        public ICollection<PageBannerItem> PageBannerItems { get; set; }
        #endregion
    }
}
