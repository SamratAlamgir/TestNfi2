using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class Prosjektinformasjon : IMember
    {
        [Required]
        [Header("Prosjektinformasjon")]
        [DisplayName("Tittel på prosjektet")]
        public string TittelPåProsjektet { get; set; }

        [Required]
        [DisplayName("Er prosjektet et originalverk")]
        public string ErProsjektetEtOriginalverk { get; set; }

        [Required, NotVisible, FileSize, JsonIgnore]
        [DisplayName("Dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public List<HttpPostedFileBase> DokumentasjonPåHovedprodusentenHarOpsjonFilmrett { get; set; }
        public List<string> DokumentasjonPåHovedprodusentenHarOpsjonFilmrettPaths { get; set; }

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

        [Required, FileSize, JsonIgnore]
        [DisplayName("Reginotat")]
        public HttpPostedFileBase Reginotat { get; set; }
        public string ReginotatPath { get; set; }

        [Required, NotVisible, FileSize, JsonIgnore]
        [DisplayName("Synopsis/handlingsbeskrivelse (1 A4 side)")]
        public HttpPostedFileBase HandlingsbeskrivelseSynopsis { get; set; }
        public string HandlingsbeskrivelseSynopsisPath { get; set; }


        [DisplayName("Manuskript")]
        [Required, NotVisible, FileSize, JsonIgnore]
        public HttpPostedFileBase Manuskript { get; set; }
        public string ManuskriptPath { get; set; }
      
        [DisplayName("Fremdriftsplan")]
        [Required, NotVisible, FileSize, JsonIgnore]
        public HttpPostedFileBase Fremdriftsplan { get; set; }
        public string FremdriftsplanPath { get; set; }
      
        [DisplayName("Opptaksplan")]
        [Required, NotVisible, FileSize, JsonIgnore]
        public HttpPostedFileBase Opptaksplan { get; set; }
        public string OpptaksplanPath { get; set; }

        [Required, NotVisible, FileSize, JsonIgnore]
        [DisplayName("Samproduksjonsavtale eller intensjonsavtale om samproduksjon")]
        public List<HttpPostedFileBase> SamproduksjonsavtaleEllerIntensjonsavtaleOmSamproduksjon { get; set; }
        public List<string> SamproduksjonsavtaleEllerIntensjonsavtaleOmSamproduksjonPaths { get; set; }


        [DisplayName("Eventuelle avtaler med distributører, tv-kanaler eller andre medietjenester")]
        [Required, NotVisible, FileSize, JsonIgnore]
        public List<HttpPostedFileBase> EventuelleAvtalerMedDistributører { get; set; }
        public List<string> EventuelleAvtalerMedDistributørerPaths { get; set; }

        [DisplayName("Eventuell kommentar til prosjektinformasjon")]
        public string EventuellKommentarTilProsjektinformasjon { get; set; }
    }
}