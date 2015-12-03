using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NFI.Models
{
    public class Prosjektinformasjon
    {
        [Required]
        [Header("Prosjektinformasjon")]
        [DisplayName("Tittel på prosjektet")]
        public string TittelPåProsjektet { get; set; }

        [Required]
        [DisplayName("Er prosjektet et originalverk")]
        public string ErProsjektetEtOriginalverk { get; set; }

        [Required, FileSize(1024 * 1024 * 10)]
        [DisplayName("Dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public List<HttpPostedFileBase> DokumentasjonPåHovedprodusentenHarOpsjonFilmrett { get; set; }

        [Required]
        [DisplayName("Språk")]
        public string Språk { get; set; }

        [Required]
        [DisplayName("Date for opptaksstart")]
        public string DateForOpptaksstart { get; set; }

        [Required]
        [DisplayName("Antatt ferdigstillelse- eller premiereDate")]
        public string AntattFerdigstillelseEllerPremiereDate { get; set; }

        [Required]
        [DisplayName("Kort beskrivelse av prosjektet ('one - liner'), maks 25 ord")]
        public string KortBeskrivelseAvProsjektet { get; set; }

        [Required]
        [DisplayName("Sjanger")]
        public string Sjanger { get; set; }

        [Required]
        [DisplayName("Lengde")]
        public string Lengde { get; set; }

        [Required]
        [NotVisible]
        [DisplayName("Reginotat")]
        public HttpPostedFileBase Reginotat { get; set; }
        public string ReginotatPath { get; set; }

        [Required]
        [NotVisible]
        [DisplayName("Synopsis/handlingsbeskrivelse (1 A4 side)")]
        public HttpPostedFileBase HandlingsbeskrivelseSynopsis { get; set; }
        public string HandlingsbeskrivelseSynopsisPath { get; set; }

        [Required]
        [NotVisible]
        [DisplayName("Manuskript")]
        public HttpPostedFileBase Manuskript { get; set; }
        public string ManuskriptPath { get; set; }
        [Required]
        [NotVisible]
        [DisplayName("Fremdriftsplan")]
        public HttpPostedFileBase Fremdriftsplan { get; set; }
        public string FremdriftsplanPath { get; set; }
        [Required]
        [NotVisible]
        [DisplayName("Opptaksplan")]
        public HttpPostedFileBase Opptaksplan { get; set; }
        public string OpptaksplanPath { get; set; }

        [Required]
        [NotVisible]
        [DisplayName("Samproduksjonsavtale eller intensjonsavtale om samproduksjon")]
        public List<HttpPostedFileBase> SamproduksjonsavtaleEllerIntensjonsavtaleOmSamproduksjon { get; set; }
        public List<string> SamproduksjonsavtaleEllerIntensjonsavtaleOmSamproduksjonPath { get; set; }


        [DisplayName("Eventuelle avtaler med distributører, tv-kanaler eller andre medietjenester")]
        [NotVisible]
        public List<HttpPostedFileBase> EventuelleAvtalerMedDistributører { get; set; }
        public List<string> EventuelleAvtalerMedDistributørerPath { get; set; }

        [DisplayName("Eventuell kommentar til prosjektinformasjon")]
        public string EventuellKommentarTilProsjektinformasjon { get; set; }
    }
}