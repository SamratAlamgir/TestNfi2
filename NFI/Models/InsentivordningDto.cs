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
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Produksjonsforetakets navn")]
        public string ProduksjonsforetaketsNavn { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Organisasjonsnummer")]
        public string OrganisasjonsNummer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postadresse")]
        public string OrganisasjonsPostadresse { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postnummer")]
        public string OrganisasjonsPostnummer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Poststed")]
        public string OrganisasjonsPoststed { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Land")]
        public string OrganisasjonsLand { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Hovedprodusentens navn")]
        public string HovedprodusentensNavn { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Hovedprodusentens kjønn")]
        public string Hovedprodusentenskjønn { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Hovedprodusentens tittel")]
        public string HovedprodusentensTittel { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Hovedprodusentens telefon")]
        public string HovedprodusentensTelefon { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Hovedprodusentens mobiltelefon")]
        public string HovedprodusentensMobiltelefon { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Hovedprodusentens e-postadresse")]
        public string HovedprodusentensEpostadresse { get; set; }

        [DisplayName("Hovedproduksjonsforetakets hjemmeside")]
        public string HovedproduksjonsforetaketsHjemmeside { get; set; }
        
        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved Certificate of origin for hovedproduksjonsselskap")]
        public HttpPostedFileBase LeggCertificateOriginForHovedproduksjonsselskap { get; set; }
        [DisplayName("Legg ved Certificate of origin for hovedproduksjonsselskap")]
        public string LeggCertificateOriginForHovedproduksjonsselskapPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"),  FileSize]
        [DisplayName("Legg ved hovedprodusentens CV")]
        public HttpPostedFileBase LeggHovedprodusentensCv { get; set; }
        [DisplayName("Legg ved hovedprodusentens CV")]
        public string LeggHovedprodusentensCvPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
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

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Søkers epost-adresse")]
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
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Tittel på prosjektet")]
        public string TittelpåProsjektet { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Er prosjektet et originalverk?")]
        public string ErProsjektetOriginalverk { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public HttpPostedFileBase LeggvedDokumentasjonHovedprodusenten { get; set; }
        [DisplayName("Legg ved dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public string LeggvedDokumentasjonHovedprodusentenPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Format")]
        public string Format { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Sjanger")]
        public string Sjanger { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Lengde")]
        public string Lengde { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Språk")]
        public string Språk { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Dato for opptaksstart i Norge")]
        public DateTime DatoForOpptaksstartNorge { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Antatt siste opptaksdag i Norge")]
        public DateTime AntattSisteOpptaksdagNorge { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved utfylt kultur-og produksjonstest")]
        public HttpPostedFileBase LeggvedUtfyltkulturProduksjonstest { get; set; }
        [DisplayName("Legg ved utfylt kultur-og produksjonstest.")]
        public string LeggvedUtfyltkulturProduksjonstestPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Kort beskrivelse av handlingen, max 200 tegn")]
        public string KortBeskrivelseHandlingen { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved manuskript")]
        public HttpPostedFileBase LeggvedManuskript { get; set; }
        [DisplayName("Legg ved manuskript")]
        public string LeggvedManuskriptPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved treatment")]
        public HttpPostedFileBase LeggvedTreatment { get; set; }
        [DisplayName("Legg ved treatment")]
        public string LeggvedTreatmentPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved produksjonsplan")]
        public HttpPostedFileBase LeggvedProduksjonsplan { get; set; }
        [DisplayName("Legg ved produksjonsplan")]
        public string LeggvedProduksjonsplanPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved cast & crew liste")]
        public HttpPostedFileBase LeggvedCastCrewListe { get; set; }
        [DisplayName("Legg ved cast & crew liste")]
        public string LeggvedCastCrewListePath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved liste over locations/innspillingssteder")]
        public HttpPostedFileBase LeggvedListeOverLocations { get; set; }
        [DisplayName("Legg ved liste over locations/innspillingssteder")]
        public string LeggvedListeOverLocationsPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved liste over leverandører i Norge og EØS")]
        public HttpPostedFileBase LeggvedListeOverLeverandører { get; set; }
        [DisplayName("Legg ved liste over leverandører i Norge og EØS")]
        public string LeggvedListeOverLeverandørerPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Beskriv hvordan produksjonen er egnet til å øke de involverte filmskapernes kompetanse og evne til å lage ambisiøse og krevende prosjekter med høy kvalitet")]
        public string BeskrivHvordanProduksjonen { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Skriv inn strategi for bærekraftig og miljøvennlig innspilling")]
        public string SkrivinnStrategi { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
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
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Totalbudsjett for prosjektet i NOK")]
        public string TotalbudsjettForProsjektet { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved totalbudsjettet for prosjekte")]
        public HttpPostedFileBase LeggvedTotalbudsjettet { get; set; }
        [DisplayName("Legg ved totalbudsjettet for prosjekte")]
        public string LeggvedTotalbudsjettetPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Estimerte kostnader i Norge i NOK")]
        public string EstimerteKostnader { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Legg ved budsjett for produksjonen i Norge i NOK (samt budsjettbeløp i EU/EØS om mer enn 80% av  produksjonskostnaden er antatt å påløpe i Norge)")]
        public HttpPostedFileBase LeggvedBudsjettForProduksjonen { get; set; }
        [DisplayName("Legg ved budsjett for produksjonen i Norge i NOK (samt budsjettbeløp i EU/EØS om mer enn 80% av  produksjonskostnaden er antatt å påløpe i Norge)")]
        public string LeggvedBudsjettForProduksjonenPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
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