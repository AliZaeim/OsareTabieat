using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Admin
{
    public class BestDealVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نباید بیشتر از {1} باشد")]
        public string? Title { get; set; }
        [Display(Name = "متن")]
        [StringLength(200, ErrorMessage = "{0} نباید بیشتر از {1} باشد")]
        public string? Text { get; set; }
        [Display(Name = "تاریخ شروع")]
        [RegularExpression("1[3-4]\\d\\d\\/(1[0-2]|[1-9]|0[1-9])\\/(0[1-9]|[1-2][0-9]|3[0-1]|[1-9])($)", ErrorMessage = "تاریخ نامعتبر است !")]
        public string? StartDate { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تاریخ پایان")]
        [RegularExpression("1[3-4]\\d\\d\\/(1[0-2]|[1-9]|0[1-9])\\/(0[1-9]|[1-2][0-9]|3[0-1]|[1-9])($)", ErrorMessage = "تاریخ نامعتبر است !")]
        public string? EndDate { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        
    }
}
