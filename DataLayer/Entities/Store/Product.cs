using DataLayer.Entities.Blogs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    /// <summary>
    /// کالاهای فروشگاه
    /// </summary>
    public class Product
    {
        public Product()
        {
            ProductItems = new List<ProductItem>();
            WareHouses= new List<WareHouse>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام کالا")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Name { get; set; }
        [Display(Name = "نام انگیسی کالا")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? EnName { get; set; }
        [Display(Name = "برند")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Brand { get; set; }
        [Display(Name = "متا تگ کامنت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [MinLength(70, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد!")]
        public string? PageComment { get; set; }
        
        [Display(Name ="واحد اندازه گیری")]
        [StringLength(50,ErrorMessage ="حداکثر {0} کاراکتر وارد کنید !")]
        public string? UnitofMeasure { get; set; }
        
        [Display(Name = "توضیحات")]        
        public string? Comment { get; set; }        
        [Display(Name = "درصد تخفیف")]        
        public int? PercentDiscount { get; set; }
        [Display(Name = "مبلغ تخفیف")]        
        public int? ValueDiscount { get; set; }
        [Display(Name = "حداقل موجودی انبار جهت هشدار")]
        public int? MinimumInventory { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "عنوان صفحه")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? PageTitle { get; set; }
        [Display(Name = "برچسب نمایشی")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Label { get; set; }
        [Display(Name = "تصویر بزرگ")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BigImage { get; set; }
        [Display(Name = "توضیح تصویر بزرگ")]
        [StringLength(100, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید !")]
        public string? BigImageAlt { get; set; }
        [Display(Name = "تصویر کوچک اول")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? SmallImage1 { get; set; }
        [Display(Name = "توضیح تصویر کوچک اول")]
        [StringLength(100, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید !")]
        public string? SmallImage1Alt { get; set; }
        [Display(Name = "تصویر کوچک دوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? SmallImage2 { get; set; }
        [Display(Name = "توضیح تصویر کوچک دوم")]
        [StringLength(100, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید !")]
        public string? SmallImage2Alt { get; set; }
        /// <summary>
        /// بسته بندی - pack
        /// فله ای - bulk
        /// </summary>
        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        [Display(Name ="نوع")]
        [StringLength(50,ErrorMessage ="حداکثر {0} کاراکتر وارد کنید !")]        
        public string? ProductType { get; set; }
        [Display(Name = "برچسب‌ها")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Tags { get; set; }

        [Display(Name = "پرفروش")]
        public bool AsBestSelling { get; set; }
        [Display(Name = "رتبه فروش")]
        public int? BestSellRate { get; set; }

        [Display(Name = "ویژه")]
        public bool IsSpecial { get; set; }
        [Display(Name = "روش مصرف")]
        public string? HowToUse { get; set; }
        [Display(Name = "وزن")]
        public float? Weight { get; set; }
        
        [Display(Name = "قیمت")]
        public int? Price { get; set; }
        [Display(Name = "ویژگی ها")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Features { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? ProductGroupId { get; set; }
        [Display(Name = "نمایش در صفحه اصلی")]
        public bool ShowInMainPage { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int? ViewCount { get; set; } = 0;
        [NotMapped]
        [Display(Name = "نام کامل")]
        public string FullName    // the FullName property
        {
            get
            {
                return Name + "-" + Brand;
            }
        }
        public Guid? BlogId { get; set; }
        #region Relations
        [Display(Name = "گروه کالا")]
        
        [ForeignKey(nameof(ProductGroupId))]
        public ProductGroup? ProductGroup { get; set; }
        public ICollection<WareHouse>? WareHouses { get; set; }
        public Blog? Blog { get; set; }
        public ICollection<ProductItem>? ProductItems { get; set; }
        #endregion
        [NotMapped]
        public IEnumerable<string> TagList
        {
            get { return (Tags ?? string.Empty).Split("-"); }
        }
        [NotMapped]
        public IEnumerable<string> FeatureList
        {
            get { return (Features ?? string.Empty).Split("-"); }
        }
    }
}
