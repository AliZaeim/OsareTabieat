using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.User
{
    public class UserRole
    {
        
        [Key]
        public int URId { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int? RoleId { get; set; }

        public DateTime? RegisterDate { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        #region Relations  
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        #endregion
        [NotMapped]
        [Display(Name = "نام کامل")]
        public string FullName    // the FullName property
        {
            get
            {
                return $"{User?.Name} {User?.Family} | {Role?.RoleTitle}";
            }
        }
    }
}
