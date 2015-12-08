using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class LanseringDto : BaseAppDto
    {

        //1. Om søker
        [Required, DisplayName("Navn på ansvarlig organisasjon")]
        public string NavnpåAnsvarligOrganisasjon { get; set; }
        [Required, DisplayName("Organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }

        [Required, DisplayName("Navn på daglig leder")]
        public string NavnpåDagligLeder { get; set; }

        [Required, DisplayName("Organisasjonens virkeområde/fagfelt (eks. kultur/kunst/utdanning etc)")]
        public string OrganisasjonensVirkeområde { get; set; }

        [Required,JsonIgnore,FileSize, DisplayName("Legg ved organisasjonens / prosjektleders CV")]
        public HttpPostedFileBase LeggvedOrganisasjonensProsjektledersCV { get; set; }
        [DisplayName("Legg ved organisasjonens / prosjektleders CV")]
        public string LeggvedOrganisasjonensProsjektledersCVPath { get; set; }
        [Required, DisplayName("Postadresse")]
        public string Postadresse { get; set; }

        [Required, DisplayName("Postnummer")]
        public string Postnummer { get; set; }
        [Required, DisplayName("Poststed")]
        public string Poststed { get; set; }
        [Required, DisplayName("Organisasjonens telefonnummer")]
        public string OrganisasjonensTelefonnummer { get; set; }
        [Required,EmailAddress, DisplayName("E-postadresse kontaktperson")]
        public string EpostadresseKontaktperson { get; set; }
        [ DisplayName("Mobiltelefon kontaktperson")]
        public string Mobiltelefon { get; set; }
        [DisplayName("Evt. annen informasjon om organisasjonen")]
        public string EvtannenInformasjonOmOrganisasjonen { get; set; }
        [Required, DisplayName("Har søker tidligere mottatt tilskudd fra disse midlene??")]
        public string Harsøkertidligere { get; set; }
        [DisplayName("Hvis JA, har Norsk filminstitutt mottatt rapport i etterkant")]
        public string HvisJAharNorsk { get; set; }

        [Required, DisplayName("Har søker gjennomført dette eller tilsvarende prosjekter tidligere?")]
        public string HarTilsvarendeProsjekterTidligere { get; set; }


        [DisplayName("Hvis JA, eksemplifiser her")]
        public string HvisJAEksemplifiserher { get; set; }

        [JsonIgnore,FileSize,DisplayName("Legg ved eventuelle referanser på gjennomføringsevne fra tidligere oppdragsgivere.")]
        public HttpPostedFileBase LeggvedEventuelle { get; set; }

        [DisplayName("Legg ved eventuelle referanser på gjennomføringsevne fra tidligere oppdragsgivere.")]
        public string LeggvedEventuellePath { get; set; }


        // 2. Om prosjektet
        [Required, DisplayName("Prosjektets tittel")]
        public string ProsjektetsTittel { get; set; }
        [Required, DisplayName("Hvilke(t) tiltak skal gjennomføres? Kort beskrivelse.")]
        public string HvilkeTiltak { get; set; }

        [JsonIgnore,Required,FileSize, DisplayName("Legg ved prosjektbeskrivelse")]
        public HttpPostedFileBase LeggvedProsjektbeskrivelse { get; set; }

        [DisplayName("Legg ved prosjektbeskrivelse")]
        public string LeggvedProsjektbeskrivelsePath { get; set; }

        [Required, DisplayName("På hvilken arena skal prosjektet gjennomføres? Oppgi sted")]
        public string PåhvilkenArena { get; set; }
        [Required, DisplayName(" Når skal prosjektet gjennomføres")]
        public string NårskalProsjektetGjennomføres { get; set; }

        [Required, DisplayName("Hva er hovedmålsetningen til prosjektet? Kort beskrivelse.")]
        public string HvaerHovedmålsetningen { get; set; }
        [Required, DisplayName(" Hvilken målgruppe(r) har prosjektet?")]
        public string HvilkenMålgruppe { get; set; }

        [DisplayName(" Oppgi prosjektets samarbeidspartnere, deltakere eller andre som omfattes av prosjektet.")]
        public string OppgiProsjektets { get; set; }

        [DisplayName(" Hva er den forventede samløede effekten av tiltaket for samarbeidspartnerne?")]
        public string HvaerdenForventede { get; set; }
        // 3. Økonomi

        [Required, DisplayName("Totalbudsjett for prosjektet i NOK.")]
        public string TotalbudsjettforProsjektet { get; set; }
        [Required, DisplayName("Søknadssum til NFI i NOK.")]
        public string SøknadssumtilNFI { get; set; }
        [Required, DisplayName("Oppgi størrelsen på egenandel i NOK. Egenandelen dekkes av søker.")]
        public string Oppgistørrelsen { get; set; }
        [Required, DisplayName("Oppgi fordelingen for andre bidragsytere / søknadsinstitusjoner.Skriv navn, beløp.")]
        public string Oppgifordelingen { get; set; }

        [Required,JsonIgnore,FileSize, DisplayName("Legg ved budsjett for prosjektet.")]
        public HttpPostedFileBase LeggedBudsjettProsjektet { get; set; }

        [DisplayName("Legg ved budsjett for prosjektet.")]
        public string LeggedBudsjettProsjektetPath { get; set; }

        [JsonIgnore,Required, DisplayName("Legg ved finansieringsplan.")]
        public HttpPostedFileBase LeggvedFinansieringsplan { get; set; }

        [DisplayName("Legg ved finansieringsplan.")]
        public string LeggvedFinansieringsplanPath { get; set; }

        //4. Eventuelle andre vedlegg

        [FileSize,JsonIgnore,DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:")]
        public List<HttpPostedFileBase> Harduvedleggsom { get; set; }

        [DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:")]
        public List<string> HarduvedleggsomPaths { get; set; }

        [DisplayName("Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Eventuelle andre vedlegg.")]
        public string Beskrivelseavandrevedlegg { get; set; }
    }
}