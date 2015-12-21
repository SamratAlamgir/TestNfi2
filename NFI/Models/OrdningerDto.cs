using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class OrdningerDto : BaseAppDto
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

        [Required, JsonIgnore, FileSize, DisplayName("Kvalifikasjoner som arrangør i form av CV")]
        public HttpPostedFileBase KvalifikasjonersomArrangørCV { get; set; }

        [DisplayName("Kvalifikasjoner som arrangør i form av CV")]
        public string KvalifikasjonersomArrangørCVPath { get; set; }

        [DisplayName("Evt.annen informasjon om organisasjonen")]
        public string Evtanneninformasjonomorganisasjonen { get; set; }

        [Required, DisplayName("Har søker tidligere mottatt tilskudd for en av disse ordningene? Lokale film- og kino tiltak, Filmfestival under 50 000, Filmkulturelle tiltak?")]
        public string Harsøkertidligere { get; set; }

        [DisplayName("Hvis JA, hvilken av ordningene?")]
        public string HvisJAhvilkenavordningene { get; set; }

        [JsonIgnore, FileSize, DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her.")]
        public HttpPostedFileBase HvisJalegg { get; set; }

        [DisplayName("Hvis Ja, legg ved rapport på gjennomført tiltak her.")]
        public string HvisJaleggPath { get; set; }

        //2. Om prosjektet
        [Required, DisplayName(" Prosjektets tittel")]
        public string Prosjektetstittel { get; set; }

        [Required, DisplayName("Beskriv kort prosjektet, max 200 tegn")]
        public string Beskrivkortprosjektet { get; set; }

        [Required, JsonIgnore, FileSize, DisplayName("Legg ved prosjektbeskrivelse")]
        public HttpPostedFileBase Leggvedprosjektbeskrivelse { get; set; }

        [DisplayName("Legg ved prosjektbeskrivelse")]
        public string LeggvedprosjektbeskrivelsePath { get; set; }

        [Required, DisplayName("Hvilken ordning søkes det tilskudd til?")]
        public string Hvilkenordningsøkes { get; set; }

        [DisplayName("Ved valg av filmkulturelle tiltak på forrige spørsmål, presiser nærmere her")]
        public string Vedvalgavfilmkulturelletiltak { get; set; }

        [Required, DisplayName("Har ansvarlig organisasjon gjennomført dette eller tilsvarende prosjekt tidligere")]
        public string Haransvarligorganisasjon { get; set; }

        [DisplayName("Hvis JA, skriv tittel  og når (Tittel, åååå)")]
        public string HvisJAskriv { get; set; }

        //3. Nedslagsfelt

        [Required, DisplayName("Hvor bredt er tiltaket tenkt å nå geografisk? Velg")]
        public string Hvorbredttiltaket { get; set; }
        [Required, DisplayName("Angi geografisk nedslagsfelt, type Norge/ Vestlandet.")]
        public string Angigeografisk { get; set; }


        //3. Tidsramme
        [Required, DisplayName("Startdato")]
        public string NårskalprosjektetFromDate { get; set; }

        [Required, DisplayName("Sluttdato")]
        public string NårskalprosjektetToDate { get; set; }

        //4. Målgruppe(r)
        //Hvilke målgrupper ønsker dere primært å nå? Prioriter med tall.*
        //1. Det allmenne publikum, filminteresserte voksne
        [Required, Range(1, 6), DisplayName("Det allmenne publikum, filminteresserte voksne")]
        public int Detallmennepublikum { get; set; }
        //2. Filmstudenter, pedagoger, andre faggrupper
        [Required, Range(1, 6), DisplayName("Filmstudenter, pedagoger, andre faggrupper")]
        public int Filmstudenterpedagoger { get; set; }
        //  3. Semiprofesjonelle / profesjonelle filmarbeidere
        [Required, Range(1, 6), DisplayName("Semiprofesjonelle / profesjonelle filmarbeidere")]
        public int Semiprofesjonelleprofesjonelle { get; set; }
        //4. Flerkulturelle miljøer eller minoritetsgrupper, spesifiser i tekstfelt
        [Required, Range(1, 6), DisplayName("Flerkulturelle miljøer eller minoritetsgrupper, spesifiser i tekstfelt")]
        public int Flerkulturellemiljøer { get; set; }
        //5. Barn og unge
        [Required, Range(1, 6), DisplayName("Barn og unge")]
        public int Barnogunge { get; set; }
        //6. Andre, spesifer i tekstfelt
        [Required, Range(1, 6), DisplayName("Andre, spesifer i tekstfelt")]
       
        public int Andrespesiferitekstfelt { get; set; }
        [Required, DisplayName("Andre")]
        public string AndrespesiferitekstfeltName { get; set; }
        [Required, DisplayName("Beskriv kort målet for prosjektet med tanke på prioritert målgruppe")]
        public string Beskrivkortmålet { get; set; }


        //5. Økonomi
        public Økonomi Økonomi { get; } = new Økonomi();

        //7. Eventuelle andre vedlegg
        [JsonIgnore, FileSize, DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:")]
        public List<HttpPostedFileBase> Harduvedleggsom { get; set; }
        [DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:")]
        public List<string> HarduvedleggsomPaths { get; set; }

        [DisplayName("Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Eventuelle andre vedlegg.")]
        public string Beskrivelseavandrevedlegg { get; set; }

        
    }
}