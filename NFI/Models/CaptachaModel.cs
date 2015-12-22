using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class CaptachaModel
    {
        //[Required]
        [Display(Name = "Skriv svaret her?")]
        public string Captcha { get; set; }

        public string Prefix { get; set; }

    }
}