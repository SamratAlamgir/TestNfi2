using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class InsentivordningDto : BaseAppDto
    {
        // 1. Kontaktinformasjon hovedprodusent:
        [Required]
        [DisplayName("Produksjonsforetakets navn")]
        public string ProduksjonsforetaketsNavn { get; set; }

        [Required]
        [DisplayName("Organisasjonsnummer")]
        public string OrganisasjonsNummer { get; set; }

        [Required]
        [DisplayName("Postadresse")]
        public string OrganisasjonsPostadresse { get; set; }

        [Required]
        [DisplayName("Postnummer")]
        public string OrganisasjonsPostnummer { get; set; }

        [Required]
        [DisplayName("Poststed")]
        public string OrganisasjonsPoststed { get; set; }

        [Required]
        [DisplayName("Land")]
        public string OrganisasjonsLand { get; set; }

        [Required]
        [DisplayName("Hovedprodusentens navn")]
        public string HovedprodusentensNavn { get; set; }

        [Required]
        [DisplayName("Hovedprodusentens kjønn")]
        public string Hovedprodusentenskjønn { get; set; }

        [Required]
        [DisplayName("Hovedprodusentens tittel")]
        public string HovedprodusentensTittel { get; set; }

        [Required]
        [DisplayName("Hovedprodusentens telefon")]
        public string HovedprodusentensTelefon { get; set; }

        [Required]
        [DisplayName("Hovedprodusentens mobiltelefon")]
        public string HovedprodusentensMobiltelefon { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Ugyldig e-postadresse")]
        [DisplayName("Hovedprodusentens e-postadresse")]
        public string HovedprodusentensEpostadresse { get; set; }

        [DisplayName("Hovedproduksjonsforetakets hjemmeside")]
        public string HovedproduksjonsforetaketsHjemmeside { get; set; }
        
        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved firmaattest/registerutskrift fra enhetsregisteret")]
        public HttpPostedFileBase LeggvedFirmaattest { get; set; }
        [DisplayName("Legg ved firmaattest/registerutskrift fra enhetsregisteret")]
        public string LeggvedFirmaattestPath { get; set; }

        [JsonIgnore, Required,  FileSize]
        [DisplayName("Legg ved hovedprodusentens CV")]
        public HttpPostedFileBase LeggHovedprodusentensCv { get; set; }
        [DisplayName("Legg ved hovedprodusentens CV")]
        public string LeggHovedprodusentensCvPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved  hovedproduksjonsselskapets track record")]
        public HttpPostedFileBase LeggHovedproduksjonsselskapetsTrackRecord { get; set; }
        [DisplayName("Legg ved  hovedproduksjonsselskapets track record")]
        public string LeggHovedproduksjonsselskapetsTrackRecordPath { get; set; }

        // 2. Kontaktinformasjon søker: / hvis annen enn hovedprodusent
        [DisplayName("Søkers navn")]
        public string SøkersNavn { get; set; }
        [DisplayName("Søkers tittel")]
        public string SøkersTittel { get; set; }
        [DisplayName("Søkers telefon")]
        public string SøkersTelefon { get; set; }
        [DisplayName("Søkers mobiltelefon")]
        public string SøkersMobiltelefon { get; set; }

        [EmailAddress(ErrorMessage = "Ugyldig e-postadresse")]
        [DisplayName("Søkers e-postadresse")]
        public string SøkersEpostAdresse { get; set; }

        [DisplayName("Produksjonsforetakets navn")]
        public string SøkersProduksjonsforetaketsNavn { get; set; }
        [DisplayName("Organisasjonsnummer")]
        public string SøkersOrganisasjonsNummer { get; set; }
        [DisplayName("Postadresse")]
        public string SøkersPostadresse { get; set; }
        [DisplayName("Postnummer")]
        public string SøkersPostnummer { get; set; }
        [DisplayName("Poststed")]
        public string SøkersPoststed { get; set; }
        [DisplayName("Land")]
        public string SøkersLand { get; set; }
        [DisplayName("Produksjonsforetakets hjemmeside")]
        public string ProduksjonsforetaketsHjemmeside { get; set; }

        [JsonIgnore, FileSize]
        [DisplayName("Last opp erklæring fra hovedprodusent på at søker kan søke på vegne av hovedprodusent")]
        public HttpPostedFileBase LastoppErklæring { get; set; }
        [DisplayName("Last opp erklæring fra hovedprodusent på at søker kan søke på vegne av hovedprodusent")]
        public string LastoppErklæringPath { get; set; }

        // 3. Prosjektinformasjon:
        [Required]
        [DisplayName("Tittel på prosjektet")]
        public string TittelpåProsjektet { get; set; }
        [Required]
        [DisplayName("Er prosjektet et originalverk?")]
        public string ErProsjektetOriginalverk { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public HttpPostedFileBase LeggvedDokumentasjonHovedprodusenten { get; set; }
        [DisplayName("Legg ved dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public string LeggvedDokumentasjonHovedprodusentenPath { get; set; }

        [Required]
        [DisplayName("Format")]
        public string Format { get; set; }
        [Required]
        [DisplayName("Sjanger")]
        public string Sjanger { get; set; }
        [Required]
        [DisplayName("Lengde")]
        public string Lengde { get; set; }
        [Required]
        [DisplayName("Språk")]
        public string Språk { get; set; }
        [Required]
        [DisplayName("Dato for opptaksstart i Norge")]
        public DateTime DatoForOpptaksstartNorge { get; set; }

        [Required]
        [DisplayName("Antatt siste opptaksdag i Norge")]
        public DateTime AntattSisteOpptaksdagNorge { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved utfylt kvalifiseringstest")]
        public HttpPostedFileBase LeggvedUtfyltkulturProduksjonstest { get; set; }
        [DisplayName("Legg ved utfylt kvalifiseringstest")]
        public string LeggvedUtfyltkulturProduksjonstestPath { get; set; }

        [Required]
        [DisplayName("Kort beskrivelse av handlingen, max 200 tegn")]
        public string KortBeskrivelseHandlingen { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved manuskript")]
        public HttpPostedFileBase LeggvedManuskript { get; set; }
        [DisplayName("Legg ved manuskript")]
        public string LeggvedManuskriptPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved treatment/sesongbue")]
        public HttpPostedFileBase LeggvedTreatment { get; set; }
        [DisplayName("Legg ved treatment/sesongbue")]
        public string LeggvedTreatmentPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved produksjonsplan")]
        public HttpPostedFileBase LeggvedProduksjonsplan { get; set; }
        [DisplayName("Legg ved produksjonsplan")]
        public string LeggvedProduksjonsplanPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved cast & crew liste")]
        public HttpPostedFileBase LeggvedCastCrewListe { get; set; }
        [DisplayName("Legg ved cast & crew liste")]
        public string LeggvedCastCrewListePath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved liste over locations/innspillingssteder")]
        public HttpPostedFileBase LeggvedListeOverLocations { get; set; }
        [DisplayName("Legg ved liste over locations/innspillingssteder")]
        public string LeggvedListeOverLocationsPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved liste over leverandører i Norge og EØS")]
        public HttpPostedFileBase LeggvedListeOverLeverandører { get; set; }
        [DisplayName("Legg ved liste over leverandører i Norge og EØS")]
        public string LeggvedListeOverLeverandørerPath { get; set; }

        [Required]
        [DisplayName("Beskriv hvordan produksjonen er egnet til å øke de involverte filmskapernes kompetanse og evne til å lage ambisiøse og krevende prosjekter med høy kvalitet")]
        public string BeskrivHvordanProduksjonen { get; set; }
        [Required]
        [DisplayName("Skriv inn strategi for bærekraftig og miljøvennlig innspilling")]
        public string SkrivinnStrategi { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved distribusjonsplan")]
        public HttpPostedFileBase LeggvedDistribusjonsPlan { get; set; }
        [DisplayName("Legg ved distribusjonsplan")]
        public string LeggvedDistribusjonsPlanPath { get; set; }

        [JsonIgnore, FileSize]
        [DisplayName("Legg internasjonal distribusjonsavtale")]
        public HttpPostedFileBase LegginternasjonalDistribusjonsavtale { get; set; }
        [DisplayName("Legg internasjonal distribusjonsavtale")]
        public string LegginternasjonalDistribusjonsavtalePath { get; set; }

        // 4. Finansieringsinformasjon: 
        [Required]
        [DisplayName("Totalbudsjett for prosjektet i NOK")]
        public string TotalbudsjettForProsjektet { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved totalbudsjett for prosjektet")]
        public HttpPostedFileBase LeggvedTotalbudsjettet { get; set; }
        [DisplayName("Legg ved totalbudsjett for prosjektet")]
        public string LeggvedTotalbudsjettetPath { get; set; }

        [Required]
        [DisplayName("Estimerte kostnader i Norge i NOK")]
        public string EstimerteKostnader { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved budsjett for produksjonen i Norge i NOK (samt budsjettbeløp i EU/EØS om mer enn 80% av  produksjonskostnaden er antatt å påløpe i Norge)")]
        public HttpPostedFileBase LeggvedBudsjettForProduksjonen { get; set; }
        [DisplayName("Legg ved budsjett for produksjonen i Norge i NOK (samt budsjettbeløp i EU/EØS om mer enn 80% av  produksjonskostnaden er antatt å påløpe i Norge)")]
        public string LeggvedBudsjettForProduksjonenPath { get; set; }

        [JsonIgnore, Required, FileSize]
        [DisplayName("Legg ved finansieringsplan (med spesifisering av private og offentlige midler samt angitt bekreftet/ubekreftet finansiering)")]
        public HttpPostedFileBase LeggvedFinansieringsplan { get; set; }
        [DisplayName("Legg ved finansieringsplan (med spesifisering av private og offentlige midler samt angitt bekreftet/ubekreftet finansiering)")]
        public string LeggvedFinansieringsplanPath { get; set; }

        [DisplayName("Prosentandel av finansieringen som er bekreftet")]
        public string ProsentandelFinansieringen { get; set; }

        // 5. Eventuelle andre vedlegg
        [JsonIgnore, FileSize]
        [DisplayName("Har du vedlegg som er relevante til søknaden som du ikke har fått lastet opp? Legg de ved her")]
        public List<HttpPostedFileBase> HarduVedleggSomerRelevante { get; set; }
        [DisplayName("Har du vedlegg som er relevante til søknaden som du ikke har fått lastet opp? Legg de ved her")]
        public List<string> HarduVedleggSomerRelevantePaths { get; set; }

        [DisplayName("Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Eventuelle andre vedlegg.")]
        public string BeskrivelseavAndreVedlegg { get; set; }
    }
}