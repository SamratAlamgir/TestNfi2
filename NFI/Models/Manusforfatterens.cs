using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class Manusforfatterens:IMember
    {
        [DisplayName("Manusforfatterens navn")]
        [Required]
        public string ManusforfatterensNavn { get; set; }
        [Required]
        [DisplayName("Manusforfatterens kjønn")]
        public string ManusforfatterensKjønn { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("Manusforfatterens CV")]
        public HttpPostedFileBase ManusforfatterensCV { get; set; }
        public string ManusforfatterensCVPath { get; set; }
    }
}