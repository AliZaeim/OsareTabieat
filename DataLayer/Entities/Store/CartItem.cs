using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class CartItem
    {
        public CartItem()
        {
            
        }
        [Key]
        public int Id { get; set; }
        [Display(Name ="آیتم محصول")]
        public int? ProductItemId{ get; set; }
        [Display(Name = "محصول")]
        public int? ProductId { get; set; }

        [Display(Name ="تعداد")]
        public int Quantity { get; set; }
        [Display(Name ="قیمت")]
        public int Price { get; set; }
        [Display(Name = "تخفیف")]
        public int? Discount { get; set; }
        [Display(Name = "قیمت خالص")]
        public int NetPrice { get; set; }
        public Guid? CartId { get; set; }
        [Display(Name = "توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
        #region Relations
        [ForeignKey(nameof(ProductItemId))]
        public ProductItem? ProductItem { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart? Cart { get; set; }
        #endregion
    }
}
