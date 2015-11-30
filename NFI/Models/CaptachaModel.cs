using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class CaptachaModel
    {
        //[Required]
        [Display(Name = "What is the sum?")]
        public string Captcha { get; set; }
    }
}