using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NFI.Models
{
    public class Sorfond
    {
        //1. Kontaktinformasjon norsk minoritetsprodusent:
        public NorskMinoritetsprodusent NorskMinoritetsprodusent { get; set; } = new NorskMinoritetsprodusent();
        //2. Kontaktinformasjon hovedprodusent:
        public HovedProdusent HovedProdusent { get; } = new HovedProdusent();
        //  3. Prosjektinformasjon
        public Prosjektinformasjon Prosjektinformasjon { get; } = new Prosjektinformasjon();

        //   4. Informasjon om personer/roller legges inn her
        public InformasjonOmPersonerRoller InformasjonOmPersonerRoller { get; } = new InformasjonOmPersonerRoller();

        //   4. Finansierings informasjon legges inn her
        public FinansieringsInformasjon FinansieringsInformasjon { get; } = new FinansieringsInformasjon();
        //   5. DAC-listen
        public DacListen DacListen { get; } = new DacListen();
        //6. Visuelt materiale
        public VisueltMateriale VisueltMateriale { get; } = new VisueltMateriale();

        //7. Eventuelle andre vedlegg
        [DisplayName("Eventuelle andre vedlegg")]
        public EventuelleAndreVedlegg EventuelleAndreVedlegg { get; } = new EventuelleAndreVedlegg();


    }
}