using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Store
{
    public class ProductGroup
    {
        public ProductGroup()
        {
            Products = new List<Product>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Title { get; set; }
        [Display(Name = "عنوان انگلیسی گروه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? EnTitle { get; set; }
        [Display(Name="تصویر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Image { get; set; }
        [Display(Name = "درصد تخفیف")]        
        public int? DiscountPercent { get; set; }
        [Display(Name = "مبلغ تخفیف")]        
        public int? DiscountValue { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "نمایش در صفحه اصلی")]
        public bool ShowInMainPage { get; set; }
        [Display(Name="والد")]
        public int? ParentId { get; set; }
        #region Relations
        [ForeignKey(nameof(ParentId))]
        [Display(Name="والد")]
        public ProductGroup? Parent { get; set; }
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
