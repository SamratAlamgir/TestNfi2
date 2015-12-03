using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class Regissoren:IMember
    {
        [Required]
        [DisplayName("Regiss�rens navn")]
        public string Regiss�rensNavnt { get; set; }
        [Required]
        [DisplayName("Regiss�rens kj�nn")]
        public string Regiss�rensKj�nn { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("Regiss�rens CV")]
        public HttpPostedFileBase Regiss�rensCV { get; set; }
        [DisplayName("Regiss�rens CV")]
        public string Regiss�rensCVPath { get; set; }
    }
}