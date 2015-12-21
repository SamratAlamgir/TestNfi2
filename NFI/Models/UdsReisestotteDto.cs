using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class UdsReisestotteDto : BaseAppDto
    {
        [Required]
        [DisplayName("Søkers navn")]
        public string Søkersnavn { get; set; }

        [Required]
        [DisplayName("Søkers postadresse")]
        public string Søkerspostadresse { get; set; }

        [Required]
        [DisplayName("Søkers poststed")]
        public string Søkerspoststed { get; set; }

        [Required]
        [DisplayName("Søkers land")]
        public string Søkersland { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Ugyldig e-postadresse")]
        [DisplayName("Søkers e-post")]
        public string Søkersepost { get; set; }
        
        [Required]
        [DisplayName("Mål for reisen? Oppgi festivalnavn / sted for filmpresentasjon")]
        public string Målforreisen { get; set; }

        [Required]
        [DisplayName("Beskrivelse av tiltak som det søkes tilskudd til")]
        public string Beskrivelse{ get; set; }


        [JsonIgnore, Required, FileSize]
        [DisplayName("Budsjett for tiltak")]
        public HttpPostedFileBase Budsjettfortiltak { get; set; }
        [DisplayName("Budsjett for tiltak")]
        public string BudsjettfortiltakPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Dokumentasjon / invitasjon til deltakelse på festival eller annen filmpresentasjon")]
        public HttpPostedFileBase Dokumentasjon { get; set; }
        [DisplayName("Dokumentasjon / invitasjon til deltakelse på festival eller annen filmpresentasjon")]
        public string DokumentasjonPath { get; set; }
    }
}