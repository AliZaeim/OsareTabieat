using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Supplementary
{
    public class BestDeal
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50,ErrorMessage ="{0} نباید بیشتر از {1} باشد")]
        public string? Title { get; set; }
        [Display(Name ="متن")]
        [StringLength(200, ErrorMessage = "{0} نباید بیشتر از {1} باشد")]
        public string? Text { get; set; }
        [Display(Name ="تاریخ شروع")]
        
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name ="تاریخ پایان")]
        
        public DateTime? EndDate { get; set; }
        [Display(Name ="فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name ="تاریخ ثبت")]
        public DateTime? RegDate { get; set; }
    }
}
