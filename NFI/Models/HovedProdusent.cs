using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class HovedProdusent:IMember
    {
        [Required]
        [Header("Kontaktinformasjon hovedprodusent:")]
        [DisplayName("Produksjonsforetakets navn")]
        public string HovedprodusentProduksjonsforetaketsNavn { get; set; }

        [Required]
        [DisplayName("Organisasjonsnummer")]
        public string HovedprodusentOrganisasjonsnummer { get; set; }

        [Required]
        [DisplayName("Postadresse")]
        public string HovedprodusentPostadresse { get; set; }

        [Required]
        [DisplayName("Postnummer")]
        public string HovedprodusentPostnummer { get; set; }

        [Required]
        [DisplayName("Poststed")]
        public string HovedprodusentPoststed { get; set; }

        [Required]
        [DisplayName("Land")]
        public string HovedprodusentLand { get; set; }

        [DisplayName("Produksjonsforetakets hjemmeside ")]
        public string HovedprodusentProduksjonsforetaketsHjemmeside { get; set; }

        [Required]
        [DisplayName("Hovedprodusentens navn")]
        public string HovedprodusentensNavn { get; set; }

        [DisplayName("Hovedprodusentens tittel")]
        public string HovedprodusentensTittel { get; set; }

        [DisplayName("Hovedprodusentens adresse")]
        public string HovedprodusentensAdresse { get; set; }

        [DisplayName("Hovedprodusentens mobiltelefon ")]
        public string HovedprodusentensMobiltelefon { get; set; }

        [DisplayName("Hovedprodusentens e-post adresse ")]
        public string HovedprodusentensEPostAdresse { get; set; }

        [DisplayName("Hovedprodusentens kjønn")]
        public string Hovedprodusentenskjonn { get; set; }
    }
}