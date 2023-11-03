using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Supplementary
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        [Display(Name ="سئوال")]
        [StringLength(200,ErrorMessage ="حداکثر {0} کاراکتر وارد کنید !")]
        public string? Question { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        [Display(Name ="پاسخ")]
        [StringLength(500,ErrorMessage ="حداکثر {0} کاراکتر وارد کنید !")]
        public string? Answer { get; set; }
        [Display(Name="فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name="تاریخ ثبت")]
        public DateTime? RegDate { get; set; }

    }
}
