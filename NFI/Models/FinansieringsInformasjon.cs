using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class FinansieringsInformasjon : IMember
    {
        [Required]
        [Header("Finansierings informasjon legges inn her")]
        [DisplayName("Totalbudsjett for prosjektet, i NOK, EUR eller USD")]
        public string TotalbudsjettForProsjektet { get; set; }

        [Required]
        [DisplayName("Søknadssum til Norsk filminstitutt, i NOK")]
        public string SøknadssumNorskFilminstitutt { get; set; }

        [Required, FileSize, NotVisible, JsonIgnore]
        public HttpPostedFileBase Kalkyle { get; set; }
        public string KalkylePath { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("Finansieringsplan med spesifikasjon")]
        public HttpPostedFileBase FinansieringsplanMedSpesifikasjon { get; set; }
        public string FinansieringsplanMedSpesifikasjonPath { get; set; }

        [Required]
        [DisplayName("Bekreftet finansiering i % (må være minumum 50%, LOI ikke godkjent)")]
        public string BekreftetFinansiering { get; set; }

        [FileSize, NotVisible, JsonIgnore]
        [DisplayName("Samtlig dokumentasjon på prosjektets bekreftede finansiering oppgitt i punktet over(LOI er ikke godkjent)")]
        public List<HttpPostedFileBase> SamtligDokumentasjonPåProsjektetsBekreftede { get; set; }
        public List<string> SamtligDokumentasjonPåProsjektetsBekreftedePaths { get; set; }

        [DisplayName("Eventuell kommentar til finansiering")]
        public string EventuellKommentarTilFinansiering { get; set; }
    }
}