using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    [DisplayName("Kontaktinformasjon norsk minoritetsprodusent:")]
    public class NorskMinoritetsprodusent
    {
        [Required]
        [DisplayName("Produksjonsforetakets navn")]
        public string ProduksjonsforetaketsNavn { get; set; }

        [Required]
        [DisplayName("Organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }

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
        [DisplayName("Land")]
        public string Land { get; set; }

        [DisplayName("Produksjonsforetakets hjemmeside")]
        public string ProduksjonsforetaketsHjemmeside { get; set; }

        [DisplayName("Minoritetsprodusentens navn")]
        public string MinoritetsprodusentensNavn { get; set; }

        [DisplayName("Minoritetsprodusentens tittel")]
        public string MinoritetsprodusentensTittel { get; set; }

        [DisplayName("Minoritetsprodusentenss telefon ")]
        public string MinoritetsprodusentenssTelefon { get; set; }

        [DisplayName("Minoritetsprodusentens mobiltelefon ")]
        public string MinoritetsprodusentensMobiltelefon { get; set; }

        [DisplayName("Minoritetsprodusentens fulle adresse ")]
        public string MinoritetsprodusentensFulleAdresse { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Minoritetsprodusentens e-postadresse")]
        public string MinoritetsprodusentensEpostadresse { get; set; }

        [Required]
        [DisplayName("Minoritetsprodusentens kjønn")]
        public string MinoritetsprodusentensKjonn { get; set; }
    }
}