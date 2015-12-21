using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class FilmDto : BaseAppDto
    {
        //  1. Om søker
        [Required, DisplayName("Navn på ansvarlig organisasjon")]
        public string Navnpåansvarligorganisasjon { get; set; }
        [Required, DisplayName("Organisasjonsnummer")]
        [Range(0, 999999999, ErrorMessage = "Ugyldig Organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }

        [Required, DisplayName("Organisasjonens virkeområde/fagfelt (eks.kultur/kunst/utdanning etc)")]
        public string OrganisasjonensVirkeområde { get; set; }

        [Required, DisplayName("Postadresse")]
        public string Postadresse { get; set; }

        [Required, DisplayName("Postnummer")]
        public string Postnummer { get; set; }

        [Required, DisplayName("Poststed")]
        public string Poststed { get; set; }

        [Required, DisplayName("Organisasjonens telefonnummer")]
        public string Organisasjonenstelefonnummer { get; set; }

        [Required, DisplayName("Navn på kontaktperson for denne søknaden")]
        public string Navnpåkontaktperson { get; set; }

        [Required, EmailAddress(ErrorMessage = "Ugyldig e-postadresse"), DisplayName("E-postadresse kontaktperson")]
        public string Epostadressekontaktperson { get; set; }

        [DisplayName("Mobiltelefon kontaktperson")]
        public string Mobiltelefonkontaktperson { get; set; }

        [Required, JsonIgnore, FileSize, DisplayName("Kvalifikasjoner som distributør i form av CV")]
        public HttpPostedFileBase KvalifikasjonersomArrangørCV { get; set; }

        [DisplayName("Kvalifikasjoner som distributør i form av CV")]
        public string KvalifikasjonersomArrangørCVPath { get; set; }

        [DisplayName("Evt.annen informasjon om organisasjonen")]
        public string Evtanneninformasjonomorganisasjonen { get; set; }

        [Required, DisplayName("Har søker tidligere mottatt tilskudd til filmdistribusjon?")]
        public string Harsøkertidligere { get; set; }

        [JsonIgnore, FileSize, DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her.")]
        public HttpPostedFileBase HvisJalegg { get; set; }

        [DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her.")]
        public string HvisJaleggPath { get; set; }

        //2. Om prosjektet
        [Required, DisplayName("Hva søkes det tilskudd til?")]
        public string Hvasøkesdet { get; set; }
        [Required, DisplayName("Prosjektets tittel")]
        public string Prosjektetstittel { get; set; }

        [Required, DisplayName("Beskriv kort prosjektet, max 200 tegn")]
        public string Beskrivkortprosjektet { get; set; }

        [Required, JsonIgnore, FileSize, DisplayName("Last opp dokument med oversikt over hvilke titler det søkes tilskudd til. Last ned og benytt denne malen til å lage oversikt (word)?")]
        public HttpPostedFileBase Lastoppdokument { get; set; }

        [DisplayName("Last opp dokument med oversikt over hvilke titler det søkes tilskuddd til.")]
        public string LastoppdokumentPath { get; set; }

        //3. Distribusjonsplan

        [Required, JsonIgnore, FileSize, DisplayName("Last opp distribusjonsplan")]
        public HttpPostedFileBase Lastoppdistribusjonsplan { get; set; }

        [DisplayName("Last opp distribusjonsplan")]
        public string LastoppdistribusjonsplanPath { get; set; }
        [Required, JsonIgnore, FileSize, DisplayName("Last opp markedsplan")]
        public HttpPostedFileBase Lastoppmarkedsplan { get; set; }

        [DisplayName("Last opp markedsplan")]
        public string LastoppmarkedsplanPath { get; set; }

        [Required, DisplayName("Startdato")]
        public string NårskalprosjektetFromDate { get; set; }

        [Required, DisplayName("Sluttdato")]
        public string NårskalprosjektetToDate { get; set; }

        //4. Målgruppe(r)
        //Hvilke målgrupper ønsker dere primært å nå? Prioriter med tall.*
        //1. Voksne
        [Required, Range(1, 3), DisplayName("Voksne")]
        public int Voksne { get; set; }
        //2.Barn og familiefilm
        [Required, Range(1, 3), DisplayName("Barn og familiefilm")]
        public int Barnogfamiliefilm { get; set; }
        //3. Andre, spesifer i tesktfelt
        [Required, Range(1, 3), DisplayName("Andre, spesifer i tesktfelt")]
        public int Andrespesiferitesktfelt { get; set; }
        [Required,DisplayName("Andre")]
        public string AndrespesiferitesktfeltName { get; set; }
        //5. Økonomi
        public Økonomi Økonomi { get; } = new Økonomi();

        //6. Eventuelle andre vedlegg
        public EventuelleAndreVedlegg EventuelleAndreVedlegg { get; set; }

    }
}