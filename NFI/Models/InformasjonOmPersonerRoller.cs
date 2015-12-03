using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NFI.Models
{
    public class InformasjonOmPersonerRoller
    {
        [Header("Informasjon om personer/roller legges inn her")]
        [Required]
        [DisplayName("CV til minoritetprodusentens produksjonsforetak")]
        [NotVisible]
        public HttpPostedFileBase CVtilMinoritetprodusentensProduksjonsforetak { get; set; }
        public string CVtilMinoritetprodusentensProduksjonsforetakPath { get; set; }
        [Required]
        [NotVisible]
        [DisplayName("CV til ansvarlig minoritetsprodusent")]
        public HttpPostedFileBase CVTilAnsvarligMinoritetsprodusentforetak { get; set; }
        public string CVTilAnsvarligMinoritetsprodusentforetakPath { get; set; }
        [NotVisible]
        [Required]
        [DisplayName("CV til hovedprodusentens produksjonsforetak")]
        public HttpPostedFileBase CVtilHovedprodusentensProduksjonsforetak { get; set; }
        public string CVtilHovedprodusentensProduksjonsforetakPath { get; set; }
        [NotVisible]
        [Required]
        [DisplayName("CV til ansvarlig hovedprodusent(er)")]
        public HttpPostedFileBase CVtilAnsvarligHovedprodusent { get; set; }
        public string CVtilAnsvarligHovedprodusentPath { get; set; }
        public List<Regissoren> Regissoren { get; } = new List<Regissoren>();

        public List<Manusforfatterens> Manusforfatterens { get; } = new List<Manusforfatterens>();

        [DisplayName("Eventuell kommentar  på personer")]
        public string EventuellKommentarPåPersoner { get; set; }


    }
}