using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class InformasjonOmPersonerRoller : IMember
    {
        [Header("Informasjon om personer/roller legges inn her")]
        [DisplayName("CV til minoritetprodusentens produksjonsforetak")]
        [Required, FileSize, NotVisible, JsonIgnore]
        public HttpPostedFileBase CVtilMinoritetprodusentensProduksjonsforetak { get; set; }
        [DisplayName("CV til minoritetprodusentens produksjonsforetak")]
        public string CVtilMinoritetprodusentensProduksjonsforetakPath { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("CV til ansvarlig minoritetsprodusent")]
        public HttpPostedFileBase CVTilAnsvarligMinoritetsprodusentforetak { get; set; }
        [DisplayName("CV til ansvarlig minoritetsprodusent")]
        public string CVTilAnsvarligMinoritetsprodusentforetakPath { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("CV til hovedprodusentens produksjonsforetak")]
        public HttpPostedFileBase CVtilHovedprodusentensProduksjonsforetak { get; set; }
        [DisplayName("CV til hovedprodusentens produksjonsforetak")]
        public string CVtilHovedprodusentensProduksjonsforetakPath { get; set; }
        [Required, FileSize, NotVisible, JsonIgnore]
        [DisplayName("CV til ansvarlig hovedprodusent(er)")]
        public HttpPostedFileBase CVtilAnsvarligHovedprodusent { get; set; }
        [DisplayName("CV til ansvarlig hovedprodusent(er)")]
        public string CVtilAnsvarligHovedprodusentPath { get; set; }
        public List<Regissoren> Regissoren { get; } = new List<Regissoren>();

        public List<Manusforfatterens> Manusforfatterens { get; } = new List<Manusforfatterens>();

        [DisplayName("Eventuell kommentar  på personer")]
        public string EventuellKommentarPåPersoner { get; set; }


    }
}