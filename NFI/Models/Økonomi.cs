using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class Økonomi
    {
        [Required, DisplayName("Søknadssum i NOK")]
        public string SøknadssumiNOK { get; set; }

        [Required, DisplayName("Totalbudsjett i NOK")]
        public string TotalbudsjettiNOK { get; set; }

        [Required, DisplayName("Nevn de viktigste andre bidragsytere / tilskuddsorganisasjonene. Skriv Navn, Beløp NOK")]
        public string Nevndeviktigste { get; set; }

        [Required, JsonIgnore, FileSize, DisplayName("Legg ved budsjett og finansieringsplan")]
        public HttpPostedFileBase Leggvedbudsjett { get; set; }

        [DisplayName("Legg ved budsjett og finansieringsplan")]
        public string LeggvedbudsjettPath { get; set; }
    }
}

