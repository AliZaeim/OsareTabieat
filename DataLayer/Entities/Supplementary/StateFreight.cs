using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.Store;

namespace DataLayer.Entities.Supplementary
{
    public class StateFreight
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        [Display(Name ="وزن")]
        public float? Weight { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید !")]
        [Display(Name ="کرایه")]
        public int? Freight { get; set; }
        [Display(Name = "استان")]
        public int StateId { get; set; }
        #region Relations
        [Display(Name ="استان")]
        public State? State { get; set; }
        #endregion
    }
}
