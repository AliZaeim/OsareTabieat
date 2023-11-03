using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.Supplementary
{
    public class Slider
    {
        public Slider()
        {

        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "تصویر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Image { get; set; }
        [Display(Name = "متن اول")]        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text1 { get; set; }
        [Display(Name = "کلاس متن اول")]        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text1Class { get; set; }
        [Display(Name="مکان نمایش متن اول")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text1Dir { get; set; }
        [Display(Name = "نوع انیمیشن متن اول")]        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text1DataAnimation { get; set; }
        [Display(Name = "زمان تاخیر متن اول به میلی ثانیه")]        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text1DataDelay { get; set; }
        [Display(Name = "متن دوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text2 { get; set; }
        [Display(Name = "کلاس متن دوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text2Class { get; set; }
        [Display(Name = "مکان نمایش متن دوم")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text2Dir { get; set; }
        [Display(Name = "نوع انمیشن متن دوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text2DataAnimation { get; set; }
        [Display(Name = "زمان تاخیر متن دوم به میلی ثانیه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text2DataDelay { get; set; }
        [Display(Name = "متن سوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text3 { get; set; }
        [Display(Name = "کلاس متن سوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text3Class { get; set; }
        [Display(Name = "مکان نمایش متن سوم")]
        [StringLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text3Dir { get; set; }
        [Display(Name = "نوع انمیشن متن سوم")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text3DataAnimation { get; set; }
        [Display(Name = "زمان تاخیر متن سوم به میلی ثانیه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? Text3DataDelay { get; set; }
        [Display(Name = "متن نمایشی لینک")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? LinkText { get; set; }
        [Display(Name = "لینک")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]

        public string? LinkUrl { get; set; }
        [Display(Name = "نوع انیمیشن دکمه")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? LinkDataAnimation { get; set; }
        [Display(Name = "زمان تاخیر")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? LinkDataDelay { get; set; }
        [Display(Name = "کلاس نمایش لینک")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string? LinkClass { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
    }
}
