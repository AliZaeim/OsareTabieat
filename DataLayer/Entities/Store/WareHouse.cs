using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class WareHouse
    {
        public WareHouse()
        {

        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "تاریخ")]        
        public DateTime? RegDate { get; set; }
        
        [Display(Name = "ورود")]        
        public float? Input { get; set; }
        [Display(Name = "خروج")]        
        public float? Export { get; set; }
        [Display(Name ="زیر مجموعه کالا")]        
        public int? ProductItemId { get; set; }
        [Display(Name ="کالا")]
        public int? ProductId { get; set; }
        [Display(Name = "جزئیات سفارش")]        
        public int? OrderdetialId { get; set; }
        [Display(Name = "حذف شده")]
        public bool IsDeleted { get; set; }
        [Display(Name ="توضیحات")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? WareHouse_Comment { get; set; }
        #region Relations
        [Display(Name = "زیر مجموعه کالا")]
        [ForeignKey(nameof(ProductItemId))]
        public ProductItem? ProductItem { get; set; }
        [Display(Name = "کالا")]
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [ForeignKey(nameof(OrderdetialId))]
        [Display(Name = "جزئیات سفارش")]        
        public CartItem? CartItem { get; set; }
        #endregion
        [NotMapped]
        [Display(Name = "مانده")]
        public float Remain    // the Remain property
        {
            get
            {
                return Input.GetValueOrDefault() - Export.GetValueOrDefault();
            }
        }

    }
}
