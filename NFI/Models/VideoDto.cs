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
        [Required]
        [DisplayName("Navn på ansvarlig organisasjon")]
        public string NavnAnsvarligOrganisasjon { get; set; }

        [Required]
        [Range(0, 999999999, ErrorMessage = "Ugyldig Organisasjonsnummer")]
        [DisplayName("Organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }

        [Required]
        [DisplayName("Organisasjonens virkeområde/fagfelt (eks. kultur/kunst/utdanning etc)")]
        public string OrganisasjonensVirkeområde { get; set; }

        [Required]
        [DisplayName("Postadresse")]
        public string Postadresse { get; set; }

        [Required]
        [DisplayName("Postnummer")]
        public string Postnummer { get; set; }

        [Required]
        [DisplayName("Poststed")]
        public string Poststed { get; set; }

        [Required]
        [DisplayName("Organisasjonens telefonnummer")]
        public string OrganisasjonensTelefonnummer { get; set; }

        [Required]
        [DisplayName("Navn på kontaktperson for denne søknaden")]
        public string NavnKontaktpersonDenneSøknaden { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Ugyldig e-postadresse")]
        [DisplayName("E-postadresse kontaktperson")]
        public string Epostadressekontaktperson { get; set; }

        [DisplayName("Mobiltelefon kontaktperson")]
        public string Mobiltelefonkontaktperson { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Kvalifikasjoner som distributør i form av CV")]
        public HttpPostedFileBase KvalifikasjonersomDistributørCv { get; set; }

        [DisplayName("Kvalifikasjoner som distributør i form av CV")]
        public string KvalifikasjonersomDistributørCvPath { get; set; }

        [DisplayName("Evt. annen informasjon om organisasjonen")]
        public string EvtAnnenInformasjonOrganisasjonen { get; set; }

        [Required]
        [DisplayName("Har søker tidligere mottatt tilskudd til videodistribusjon?")]
        public string HarsøkerTidligereMottattVideodistribusjon { get; set; }

        [JsonIgnore, FileSize]
        [DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her")]
        public HttpPostedFileBase HvisJaLeggVedRapportTiltakher { get; set; }

        [DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her")]
        public string HvisJaLeggVedRapportTiltakherPath { get; set; }

        // 2. Om prosjektet
        [Required]
        [DisplayName("Prosjektets tittel")]
        public string ProsjektetsTittel { get; set; }

        [Required]
        [DisplayName("Beskriv kort prosjektet, max 200 tegn")]
        public string BeskrivkortProsjektet { get; set; }

        [JsonIgnore, FileSize]
        [DisplayName("Last opp dokument med oversikt over hvilke titler det søkes tilskuddd til.")]
        public HttpPostedFileBase LastoppDokumentMedOversikt { get; set; }

        [DisplayName("Last opp dokument med oversikt over hvilke titler det søkes tilskuddd til.")]
        public string LastoppDokumentMedOversiktPath { get; set; }

        // 3. Distribusjonsplan
        [JsonIgnore, Required, FileSize]
        [DisplayName("Last opp distribusjonsplan")]
        public HttpPostedFileBase LastoppDistribusjonsplan { get; set; }

        [DisplayName("Last opp distribusjonsplan")]
        public string LastoppDistribusjonsplanPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Last opp markedsplan")]
        public HttpPostedFileBase LastoppMarkedsplan { get; set; }

        [DisplayName("Last opp markedsplan")]
        public string LastoppMarkedsplanPath { get; set; }

        [DisplayName("Tidsramme for prosjektet? Fra - til")]
        public string TidsrammeForProsjektetFrom { get; set; }
        public string TidsrammeForProsjektetTo { get; set; }


        // 4. Målgruppe(r)
        [DisplayName("Hvilke målgrupper ønsker dere primært å nå? Prioriter med tall.")]
        public List<MalgruppeDto> MalgruppeDtoList { get; set; }

        // 5. Økonomi
        [Required]
        [DisplayName("Søknadssum i NOK")]
        public string SøknadssumNok { get; set; }

        [Required]
        [DisplayName("Totalbudsjett i NOK")]
        public string TotalbudsjettNok { get; set; }

        [Required]
        [DisplayName("Nevn eventuelt andre finansieringskilder. Skriv Navn, Beløp NOK")]
        public string NevneventueltAndreFinansieringskilder { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved budsjett og finansieringsplan")]
        public HttpPostedFileBase LeggvedBudsjettFinansieringsplan { get; set; }

        [DisplayName("Legg ved budsjett og finansieringsplan")]
        public string LeggvedBudsjettFinansieringsplanPath { get; set; }

        // 6. Eventuelle andre vedlegg
        [JsonIgnore, FileSize]
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
        public string AnnetSpesifiser { get; set; }

        public string AnnetText { get; set; }
    }
}