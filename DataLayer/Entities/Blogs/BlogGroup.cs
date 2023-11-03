using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Blogs
{
    public class BlogGroup
    {
        public BlogGroup()
        {
            Blogs = new HashSet<Blog>();
        }
        [Key]
        public int BlogGroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} باید {1} رقم باشد!")]
        public string? BlogGroupTitle { get; set; }
        [Display(Name = "عنوان انگلیسی گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} باید {1} رقم باشد!")]

        public string? BlogGroupEnTitle { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name ="حذف شده")]
        public bool IsDeleted { get; set; }
        #region Relations
        public  ICollection<Blog> Blogs { get; set; }
        #endregion
    }
}
