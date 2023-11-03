using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Store
{
    public class OrderDetail
    {
        public OrderDetail()
        {
        }
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? ProductName { get; set; }
        
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name="تعداد کالا")]
        public int? ProductCount { get; set; }
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Comment { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? OrderDetailPrice { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? OrderDetailNetPrice { get; set; }
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? OrderDetailDiscountCode { get; set; }

        public int? OrderDetailDiscountValue { get; set; }
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Note { get; set; }

        public Guid? Order_Id { get; set; }
        #region Relations
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey(nameof(Order_Id))]
        public Order? Order { get; set; }
        #endregion

    }
}
