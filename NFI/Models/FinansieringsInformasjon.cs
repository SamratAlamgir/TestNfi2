using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NFI.Models
{
    public class FinansieringsInformasjon
    {
        [Required]
        [Header("Finansierings informasjon legges inn her")]
        [DisplayName("Totalbudsjett for prosjektet, i NOK, EUR eller USD")]
        public string TotalbudsjettForProsjektet { get; set; }

        [Required]
        [DisplayName("S�knadssum til Norsk filminstitutt, i NOK")]
        public string S�knadssumNorskFilminstitutt { get; set; }

        [Required]
        [NotVisible]
        public HttpPostedFileBase Kalkyle { get; set; }
        public string KalkylePath { get; set; }
        [Required]
        [NotVisible]
        [DisplayName("Finansieringsplan med spesifikasjon")]
        public HttpPostedFileBase FinansieringsplanMedSpesifikasjon { get; set; }
        public string FinansieringsplanMedSpesifikasjonPath { get; set; }

        [Required]
        [DisplayName("Bekreftet finansiering i % (m� v�re minumum 50%, LOI ikke godkjent)")]
        public string BekreftetFinansiering { get; set; }

        [Required]
        [NotVisible]
        [DisplayName("Samtlig dokumentasjon p� prosjektets bekreftede finansiering oppgitt i punktet over(LOI er ikke godkjent)")]
        public List<HttpPostedFileBase> SamtligDokumentasjonP�ProsjektetsBekreftede { get; set; }
        public List<string> SamtligDokumentasjonP�ProsjektetsBekreftedePaths { get; set; }

        [DisplayName("Eventuell kommentar til finansiering")]
        public string EventuellKommentarTilFinansiering { get; set; }
    }
}