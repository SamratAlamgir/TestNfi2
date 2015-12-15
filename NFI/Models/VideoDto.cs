using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class VideoDto : BaseAppDto
    {
        // 1. Om søker
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Navn på ansvarlig organisasjon")]
        public string NavnAnsvarligOrganisasjon { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0, 999999999, ErrorMessage = "Please enter valid integer number")]
        [DisplayName("Organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Organisasjonens virkeområde/fagfelt (eks. kultur/kunst/utdanning etc)")]
        public string OrganisasjonensVirkeområde { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postadresse")]
        public string Postadresse { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postnummer")]
        public string Postnummer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Poststed")]
        public string Poststed { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Organisasjonens telefonnummer")]
        public string OrganisasjonensTelefonnummer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Navn på kontaktperson for denne søknaden")]
        public string NavnKontaktpersonDenneSøknaden { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("E-postadresse kontaktperson")]
        public string Epostadressekontaktperson { get; set; }

        [DisplayName("Mobiltelefon kontaktperson")]
        public string Mobiltelefonkontaktperson { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Kvalifikasjoner som distributør i form av CV")]
        public HttpPostedFileBase KvalifikasjonersomDistributørCv { get; set; }

        [DisplayName("Kvalifikasjoner som distributør i form av CV")]
        public string KvalifikasjonersomDistributørCvPath { get; set; }

        [DisplayName("Evt. annen informasjon om organisasjonen")]
        public string EvtAnnenInformasjonOrganisasjonen { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Har søker tidligere mottatt tilskudd til videodistribusjon?")]
        public string HarsøkerTidligereMottattVideodistribusjon { get; set; }

        [DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her")]
        public string HvisJaLeggVedRapportTiltakher { get; set; }

        // 2. Om prosjektet
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Prosjektets tittel")]
        public string ProsjektetsTittel { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Beskriv kort prosjektet, max 200 tegn")]
        public string BeskrivkortProsjektet { get; set; }

        [JsonIgnore, FileSize]
        [DisplayName("Last opp dokument med oversikt over hvilke titler det søkes tilskuddd til?")]
        public HttpPostedFileBase LastoppDokumentMedOversikt { get; set; }

        [DisplayName("Last opp dokument med oversikt over hvilke titler det søkes tilskuddd til?")]
        public string LastoppDokumentMedOversiktPath { get; set; }

        // 3. Distribusjonsplan
        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Last opp distribusjonsplan")]
        public HttpPostedFileBase LastoppDistribusjonsplan { get; set; }

        [DisplayName("Last opp distribusjonsplan")]
        public string LastoppDistribusjonsplanPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Last opp markedsplan")]
        public HttpPostedFileBase LastoppMarkedsplan { get; set; }

        [DisplayName("Last opp markedsplan")]
        public string LastoppMarkedsplanPath { get; set; }
        
        [DisplayName("Tidsramme for prosjektet?")]
        public string TidsrammeForProsjektetFrom { get; set; }
        public string TidsrammeForProsjektetTo { get; set; }


        // 4. Målgruppe(r)
        [DisplayName("Hvilke målgrupper ønsker dere primært å nå? Prioriter med tall.")]
        public List<MalgruppeDto> MalgruppeDtoList { get; set; }

        // 5. Økonomi
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Søknadssum i NOK")]
        public string SøknadssumNok { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Totalbudsjett i NOK")]
        public string TotalbudsjettNok { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Nevn eventuelt andre finansieringskilder. Skriv Navn, Beløp NOK")]
        public string NevneventueltAndreFinansieringskilder { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved budsjett og finansieringsplan")]
        public HttpPostedFileBase LeggvedBudsjettFinansieringsplan { get; set; }

        [DisplayName("Legg ved budsjett og finansieringsplan")]
        public string LeggvedBudsjettFinansieringsplanPath { get; set; }

        // 6. Not mentioned in document

        // 7. Eventuelle andre vedlegg
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her")]
        public List<HttpPostedFileBase> HarduVedleggSomRelevante { get; set; }

        [DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her")]
        public List<string> HarduVedleggSomRelevantePaths { get; set; }

        [DisplayName("Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Eventuelle andre vedlegg")]
        public string BeskrivelseavAndreVedlegg { get; set; }
    }

    public class MalgruppeDto
    {
        [DisplayName("1. Voksne")]
        public string Voksne { get; set; }

        [DisplayName("2. Barn og familiefilm")]
        public string BarnogFamiliefilm { get; set; }

        [DisplayName("3. Annet, spesifiser [Single line text]")]
        public string AnnetSpesifiser  { get; set; }

        public string AnnetText { get; set; }
    }
}