using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class Regissoren:IMember
    {
        [Required]
        [DisplayName("Regissørens navn")]
        public string RegissørensNavnt { get; set; }
        [Required]
        [DisplayName("Regissørens kjønn")]
        public string RegissørensKjønn { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("Regissørens CV")]
        public HttpPostedFileBase RegissørensCV { get; set; }
        [DisplayName("Regissørens CV")]
        public string RegissørensCVPath { get; set; }
    }
}