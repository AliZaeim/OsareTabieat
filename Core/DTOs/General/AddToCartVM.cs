using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class AddToCartVM
    {
        public int Id { get; set; }
        [Display(Name = "جزء محصول")]
        public int? ProductItemId { get; set; }
        [Display(Name = "محصول")]
        public int? ProductId { get; set; }
        [Display(Name = "تعداد")]
        public int Quantity { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }
        [Display(Name = "تخفیف")]
        public int? Discount { get; set; }
        [Display(Name = "قیمت خالص")]
        public int NetPrice { get; set; }
        public Guid? CartId { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
    }
}
