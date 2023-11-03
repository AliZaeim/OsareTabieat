using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Store
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? StatusId { get; set; }
        [Display(Name="توضیحات")]
        public string? Comment { get; set; }
        [Display(Name="تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
        public Guid? OrderId { get; set; }
        #region Relations
        [ForeignKey(nameof(StatusId))]
        public Status? Status { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
        #endregion
    }
}
