using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderStatuses= new HashSet<OrderStatus>();
        }
        [Key]
        [Display(Name = "شماره سفارش")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "تاریخ")]
        public DateTime? RegDate { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "استان")]
        public string? StateName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "شهرستان")]
        public string? CountyName { get; set; }
        [Display(Name = "نام خریدار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BuyerName { get; set; }
        [Display(Name = "نام خانوادگی  خریدار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BuyerFamily { get; set; }
        [Display(Name = "تلفن همراه خریدار")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? BuyerCellphone { get; set; }
        [Display(Name ="نام تحویل گیرنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? RecipientName { get; set; }
        [Display(Name ="نام خانوادگی تحویل گیرنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? RecipientFamily { get; set; }
        [Display(Name ="تلفن تحویل گیرنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? RecipientPhone { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "آدرس ")]
        public string? Address { get; set; }

        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد پستی")]
        public string? PostalCode { get; set; }
        [StringLength(30, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "کد تخفیف")]
        public string? DiscountCode { get; set; }
        [Display(Name = "مبلغ تخفیف")]
        public int? DiscountValue { get; set; }
        [Display(Name = "هزینه ارسال")]
        public int? DeliveryCost { get; set; }
        [Display(Name = "تخفیف هزینه ارسال")]
        public int? DeliveryCostDiscount { get; set; }
        [Display(Name = "جمع کل")]
        public long? OrderSum { get; set; }
        [Display(Name = "پرداخت")]
        public bool IsFinished { get; set; }
        [Display(Name = "حذف")]
        public bool IsDeleted { get; set; }
        [Display(Name = "لغو")]
        public bool IsCanceled { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "نام تجاری کاربر")]
        public string? UserBusinessName { get; set; }
        [Display(Name = "درصد تجاری کاربر")]
        public float? UserBusinessPercent { get; set; }
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        [Display(Name = "شماره اختصاصی سفارش")]
        public string? DedicatedNumber { get; set; }
        [Display(Name = "نحوه دریافت")]
        [MaxLength(100)]
        public string? DeliveryType { get; set; }
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        [Display(Name = "توضیحات")]
        public string? Comment { get; set; }
        
        [Display(Name = "شماره سبد خرید")]
        public Guid? CartId { get; set; }
        
        [Display(Name = "واحد پول")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Currency { get; set; }
        [Display(Name = "سفارش دهنده")]
        public int? UserId { get; set; }
        #region Relations
        [ForeignKey(nameof(UserId))]
        [Display(Name = "کاربر")]
        public User.User? User { get; set; }
        [ForeignKey(nameof(CartId))]
        [Display(Name = "سبد خرید")]
        public Cart? Cart { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<OrderStatus> OrderStatuses { get; set; }

        #endregion
        [NotMapped]
        public IEnumerable<string> CommentList
        {
            get { return (Comment ?? string.Empty).Split(Environment.NewLine); }
        }
        


    }
}
