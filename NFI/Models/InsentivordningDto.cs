using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class InsentivordningDto
    {
        public string AppId { get; set; }
        public string ZipFilePath { get; set; }
        public string PdfFilePath { get; set; }
        public List<string> UploadedFilesPath { get; set; }

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
        [DisplayName("Hovedprodusentens tittel")]
        public string HovedprodusentensTittel { get; set; }
        [Required]
        [DisplayName("Hovedprodusentens telefon")]
        public string HovedprodusentensTelefon { get; set; }
        [Required]
        [DisplayName("Hovedprodusentens mobiltelefon")]
        public string HovedprodusentensMobiltelefon { get; set; }
        [Required]
        [DisplayName("Hovedprodusentens e-postadresse")]
        public string HovedprodusentensEpostadresse { get; set; }
        [Required]
        [DisplayName("Hovedproduksjonsforetakets hjemmeside")]
        public string HovedproduksjonsforetaketsHjemmeside { get; set; }
        [Required]
        [DisplayName("Legg ved Certificate of origin for hovedproduksjonsselskap")]

        public string LeggCertificateOriginForHovedproduksjonsselskapPath { get; set; }

        [Required]
        [JsonIgnore]
        public HttpPostedFileBase LeggCertificateOriginForHovedproduksjonsselskap { get; set; }

        public string LeggHovedprodusentensCvPath { get; set; }

        [Required]
        [DisplayName("Legg ved hovedprodusentens CV")]
        [JsonIgnore]
        public HttpPostedFileBase LeggHovedprodusentensCv { get; set; }

        public string LeggHovedproduksjonsselskapetsTrackRecordPath { get; set; }

        [Required]
        [DisplayName("Legg ved  hovedproduksjonsselskapets track record")]
        [JsonIgnore]
        public HttpPostedFileBase LeggHovedproduksjonsselskapetsTrackRecord { get; set; }

        

        // 2. Kontaktinformasjon søker: / hvis annen enn hovedprodusent
        [DisplayName("Søkers navn")]
        public string SøkersNavn { get; set; }
        [DisplayName("Søkers tittel")]
        public string SøkersTittel { get; set; }
        [DisplayName("Søkers telefon")]
        public string SøkersTelefon { get; set; }
        [DisplayName("Søkers mobiltelefon")]
        public string SøkersMobiltelefon { get; set; }
        [Required]
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

        [DisplayName("Last opp erklæring fra hovedprodusent på at søker kan søke på vegne av hovedprodusent")]
        [JsonIgnore]
        public HttpPostedFileBase LastoppErklæring { get; set; }
        public string LastoppErklæringPath { get; set; }

        // 3. Prosjektinformasjon:

        [Required]
        [DisplayName("Tittel på prosjektet")]
        public string TittelpåProsjektet { get; set; }
        [Required]
        [DisplayName("Er prosjektet et originalverk?")]
        public string ErProsjektetOriginalverk { get; set; }
        [Required]
        [DisplayName("Legg ved dokumentasjon på at hovedprodusenten har opsjon/filmrett")]
        public HttpPostedFileBase LeggvedDokumentasjonHovedprodusenten { get; set; }
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
        [Required]
        [DisplayName("Legg ved utfylt kultur-og produksjonstest. Testen finner du her")]
        public HttpPostedFileBase LeggvedUtfyltkulturProduksjonstest { get; set; }
        [Required]
        [DisplayName("Kort beskrivelse av handlingen, max 200 tegn")]
        public string KortBeskrivelseHandlingen { get; set; }
        [Required]
        [DisplayName("Legg ved manuskript")]
        public HttpPostedFileBase LeggvedManuskript { get; set; }
        [Required]
        [DisplayName("Legg ved treatment")]
        public HttpPostedFileBase LeggvedTreatment { get; set; }
        [Required]
        [DisplayName("Legg ved produksjonsplan")]
        public HttpPostedFileBase LeggvedProduksjonsplan { get; set; }
        [Required]
        [DisplayName("Legg ved cast & crew liste")]
        public HttpPostedFileBase LeggvedCastCrewListe { get; set; }
        [Required]
        [DisplayName("Legg ved liste over locations/innspillingssteder")]
        public HttpPostedFileBase LeggvedListeOverLocations { get; set; }
        [Required]
        [DisplayName("Legg ved liste over leverandører i Norge og EØS")]
        public HttpPostedFileBase LeggvedListeOverLeverandører { get; set; }
        [Required]
        [DisplayName("Legg ved distribusjonsplan")]
        public HttpPostedFileBase LeggvedDistribusjonsPlan { get; set; }
        [Required]
        [DisplayName("Beskriv hvordan produksjonen er egnet til å øke de involverte filmskapernes kompetanse og evne til å lage ambisiøse og krevende prosjekter med høy kvalitet")]
        public string BeskrivHvordanProduksjonen { get; set; }
        [Required]
        [DisplayName("Skriv inn strategi for bærekraftig og miljøvennlig innspilling")]
        public string SkrivinnStrategi { get; set; }

        // 4. Visuelt materiale
        public IEnumerable<VisueltMaterialeDto> VisueltMaterialeList { get; set; }

        // 5. Finansieringsinformasjon: 
        [Required]
        [DisplayName("Totalbudsjett for prosjektet i NOK")]
        public string TotalbudsjettForProsjektet { get; set; }
        [Required]
        [DisplayName("Legg ved totalbudsjettet i enten NOK/EURO/USD")]
        public HttpPostedFileBase LeggvedTotalbudsjettet { get; set; }
        [Required]
        [DisplayName("Estimerte kostnader i Norge i NOK")]
        public string EstimerteKostnader { get; set; }
        [Required]
        [DisplayName("Legg ved budsjett for produksjonen i Norge i NOK (samt budsjettbeløp i EU/EØS om mer enn 80% av  produksjonskostnaden er antatt å påløpe i Norge)")]
        public HttpPostedFileBase LeggvedBudsjettForProduksjonen { get; set; }
        [Required]
        [DisplayName("Legg ved finansieringsplan (med spesifisering av private og offentlige midler samt angitt bekreftet/ubekreftet finansiering)")]
        public HttpPostedFileBase LeggvedFinansieringsplan { get; set; }
        [DisplayName("Prosentandel av finansieringen som er bekreftet")]
        public string ProsentandelFinansieringen { get; set; }

        // 6. Eventuelle andre vedlegg
        [DisplayName("Har du vedlegg som er relevante til søknaden som du ikke har fått lastet opp? Legg de ved her")]
        public IEnumerable<HttpPostedFileBase> HarduVedleggSomerRelevante { get; set; }
        [DisplayName("Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Eventuelle andre vedlegg.")]
        public string BeskrivelseavAndreVedlegg { get; set; }
    }


    public class VisueltMaterialeDto
    {
        [DisplayName("Nettadresse til eventuelt visuelt materiale (webside, Vimeo, etc)")]
        public string NettadresseEventueltVisueltMateriale { get; set; }
        [DisplayName("Oppgi evt passord til nettadresse med visuelt materiale")]
        public string OppgiEvtPassordNettadresse { get; set; }
    }
}