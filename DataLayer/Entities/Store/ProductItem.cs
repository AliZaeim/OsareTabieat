using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    /// <summary>
    /// زیر مجموعه کالا ها
    /// </summary>
    public class ProductItem
    {
        public ProductItem()
        {
            WareHouses = new List<WareHouse>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Name { get; set; }
        [Display(Name = "نام انگیسی")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? EnName { get; set; }
        

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
       
        [Display(Name = "درصد تخفیف")]        
        public int? PercentDiscount { get; set; }
        [Display(Name = "مبلغ تخفیف")]        
        public int? ValueDiscount { get; set; }  
        
        [Display(Name = "حداقل موجودی انبار جهت هشدار")]
        public int? MinimumInventory { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "پرفروش")]
        public bool AsBestSelling { get; set; }
        [Display(Name = "رتبه فروش")]
        public int? BestSellRate { get; set; }     

        [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }
        [Display(Name = "برچسب‌ها")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Tags { get; set; }
        [Display(Name = "برچسب نمایشی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Label { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        [Display(Name = "وزن")]       
        public float? Weight { get; set; }
        [Display(Name = "واحد اندازه گیری")]
        [StringLength(50, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید !")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? UnitofMeasure { get; set; }
        [Display(Name ="مقدار بر اساس واحد اندازه گیری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        public float? ValueBaseOnUoM { get; set; }
        [Display(Name = "نمایش قیمت")]
        public bool ShowAsPrice { get; set; }
        [Display(Name = "نمایش در صفحه اصلی")]
        public bool ShowInMainPage { get; set; }

        [Display(Name = "کالا")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? ProductId { get; set; }
        #region Relations
        
        [ForeignKey(nameof(ProductId))]
        [Display(Name = "کالا")]
        public Product? Product { get; set; }
        public ICollection<WareHouse>? WareHouses { get; set; }
        #endregion
        [NotMapped]        
        public IEnumerable<string> TagList
        {
            get { return (Tags ?? string.Empty).Split("-"); }
        }



    }
}
