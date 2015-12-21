using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class DenKulturelleSkolesekkenDto : BaseAppDto
    {

        public DenKulturelleSkolesekkenDto()
        {
            HarprosjektetSources = new SelectList(new[] { "Lokalt", "Fylkeskommunalt", "Nasjonalt", "Nei, har ikke" });
            HvaslagsprosjektSource = new SelectList(new[] { "Filmverksted for elever", "Møte med filmskaper", "Utvikling av prosjekt", "Filmvisninger", "Produksjon der flere kunst- og kulturuttrykk tas i bruk", "Annet" });
            ProsjektetmålgruppeSource = new SelectList(new[] { "1.-4", "5.-7", "8.-10", "Videregående skole" });
        }


        //1. Om søker
        [Required, DisplayName("Navn på ansvarlig organisasjon/ utøver")]
        public string Navnpåansvarligorganisasjon { get; set; }
        [Required, DisplayName("Organisasjonsnummer")]
        [Range(0, 999999999, ErrorMessage = "Ugyldig Organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }


        [Required, DisplayName("Organisasjonsform? (eks. stiftelse, selvstendig næringdsrivende etc)")]
        public string Organisasjonsform { get; set; }

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

        [Required, JsonIgnore, FileSize, DisplayName("Kvalifikasjoner som arrangør i form av CV")]
        public HttpPostedFileBase KvalifikasjonersomArrangørCV { get; set; }

        [DisplayName("Kvalifikasjoner som arrangør i form av CV")]
        public string KvalifikasjonersomArrangørCVPath { get; set; }

        [DisplayName("Evt.annen informasjon om organisasjonen")]
        public string Evtanneninformasjonomorganisasjonen { get; set; }

        [Required, DisplayName("Har søker tidligere mottatt tilskudd for DKS fra Film&Kino?")]
        public string Harsøkertidligere { get; set; }

        [JsonIgnore, FileSize, DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her.")]
        public HttpPostedFileBase HvisJalegg { get; set; }

        [DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her.")]
        public string HvisJaleggPath { get; set; }

        [Required, DisplayName("Har prosjektet forankring i Den kulturelle skolesekken på lokalt, fylkeskommunalt, nasjonalt nivå gjennom samarbeid, intensjonsavtaler eller økonomisk støtte?")]
        public IEnumerable<string> Harprosjektet { get; set; }
        [JsonIgnore]
        public SelectList HarprosjektetSources { get; set; }

        [DisplayName("Skriv inn et eller flere fylker. Skill med komma.")]
        public string Skrivinn { get; set; }

        [JsonIgnore, FileSize, DisplayName("Legg ved kopi av avtale som bekrefter samarbeid, intensjon, økonomisk støtte")]
        public HttpPostedFileBase KopiAvAvtale { get; set; }

        [DisplayName("Legg ved kopi av avtale som bekrefter samarbeid, intensjon, økonomisk støtte")]
        public string KopiAvAvtalePath { get; set; }

        [Required, DisplayName("Skal tilskuddet gå til utvikling eller gjennomføring av prosjektet?")]
        public string Skaltilskuddet { get; set; }

        //2. Om prosjektet

        [Required, DisplayName("Prosjektets tittel")]
        public string Prosjektetstittel { get; set; }

        [Required, DisplayName("Beskriv kort prosjektet, max 200 tegn")]
        public string Beskrivkortprosjektet { get; set; }

        [Required, JsonIgnore, FileSize, DisplayName("Legg ved prosjektbeskrivelse")]
        public HttpPostedFileBase Prosjektbeskrivelse { get; set; }

        [DisplayName("Legg ved prosjektbeskrivelse")]
        public string ProsjektbeskrivelsePath { get; set; }


        [Required, DisplayName("Hva slags prosjekt er dette?")]
        public List<string> Hvaslagsprosjekt { get; set; }

        [Required, DisplayName("Hva slags prosjekt er dette?")]
        [JsonIgnore]
        public SelectList HvaslagsprosjektSource { get; set; }


        [Required, DisplayName("Prosjektet målgruppe i skolen (Klassetrinn)")]
        public List<string> Prosjektetmålgruppe { get; set; }
        [JsonIgnore]
        public SelectList ProsjektetmålgruppeSource { get; set; }
        [Required, DisplayName("Er omfang avklart?")]
        public string Eromfangavklart { get; set; }

        [DisplayName("Hvis ja, oppgi omfang (Antall elever, klasser, skoler som prosjektet tilbys)")]
        public string Hvisjaoppgi { get; set; }
        [Required, DisplayName("Varighet (Antall timer/ dager hver elever vil bruke i forbindelse med prosjektet). Skriv Timer: xx / Dager:xx.")]
        public string Varighet { get; set; }
        [Required, DisplayName("Beskriv kort elevenes filmfaglige utbytte av prosjektet.")]
        public string Beskrivkortelevenes { get; set; }

        [Required, DisplayName("Startdato")]
        public string NårskalprosjektetFromDate { get; set; }

        [Required, DisplayName("Sluttdato")]
        public string NårskalprosjektetToDate { get; set; }

        [Required, JsonIgnore, FileSize, DisplayName("Legg ved tidsplan for prosjektet.")]
        public HttpPostedFileBase TidsplanProsjektet { get; set; }

        [DisplayName("Legg ved tidsplan for prosjektet.")]
        public string TidsplanProsjektetPath { get; set; }
        [Required, JsonIgnore, FileSize, DisplayName("Legg ved prosjektes budsjett")]
        public HttpPostedFileBase ProsjektesBudsjett { get; set; }

        [DisplayName("Legg ved prosjektes budsjett")]
        public string ProsjektesBudsjettPath { get; set; }

        [Required, DisplayName("Søknadssum i NOK")]
        [Range(0, 999999999, ErrorMessage = "Ugyldig Organisasjonsnummer")]
        public string Søknadssum { get; set; }

        [Required, DisplayName("Totalbudsjett i NOK")]
        [Range(0, 999999999, ErrorMessage = "Ugyldig Organisasjonsnummer")]
        public string Totalbudsjett { get; set; }

        [Required, DisplayName("Nevn eventuelt andre finansieringskilder.Skriv Navn, Beløp NOK")]
        public string NevnEventueltAndre { get; set; }

        public EventuelleAndreVedlegg EventuelleAndreVedlegg { get; set; }

    }
}