using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class CheckOutVM
	{
        public CheckOutVM()
        {

        }
        public string? CartId { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? BuyerName { get; set; }
        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? BuyerFamily { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? BuyerCellphone { get; set; }
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? StateId { get; set; }
        public State? State { get; set; }
        [Display(Name = "شهرستان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? CountyId { get; set; }
        public County? County { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Address { get; set; }
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? PostalCode { get; set; }
        [Display(Name = "تحویل گیرنده خودم می باشم")]
        public bool BuyerIsRecepient { get; set; } = true;
        [Display(Name ="ایجاد حساب کاربری")]
        public bool CreateAccount { get; set; }
        [Display(Name = "نام")]
        public string? RecepientName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string? RecepientFamily { get; set; }
        [Display(Name = "تلفن همراه")]
        public string? RecepientCellphone { get; set; }        
        [Display(Name = "یادداشت سفارش")]
        [StringLength(200,ErrorMessage ="حداکثر {0} کاراکتر وارد کنید !")]
        public string? Comment { get; set; }
        [Display(Name ="پرداخت کرایه هنگام تحویل (تیپاکس)")]
        public bool PaymentofFareDuringDeliveryTipax { get; set; } = true;
        [Display(Name = "پرداخت کرایه هنگام تحویل (اسنپ، فقط کرج)")]
        public bool PaymentofFareDuringDeliverySnap { get; set; } 
        [Display(Name ="ارسال با پست پیشتاز")]
        public bool ShippingByPishtazPost { get; set; }
        [Display(Name ="جمع کل")]
        public int CartTotal { get; set; }
        public int CartTotalWeight { get; set; }

        public List<State> States { get; set; } = new();
        public List<County> Counties { get; set; } = new();
    }
}
