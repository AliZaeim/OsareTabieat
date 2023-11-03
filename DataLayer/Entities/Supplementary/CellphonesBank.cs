using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Supplementary
{
    public class CellphonesBank
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name="تلفن همراه")]
        [RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$", ErrorMessage = " شماره تلفن همراه نا معتبر است !")]
        public string? Cellphone { get; set; }
        [Display(Name="تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

    }
}
