using System.Collections.Generic;
using System.ComponentModel;

namespace NFI.Models
{
    public class VisueltMateriale:IMember
    {
        [Header("Visuelt materiale")]
        [DisplayName("Inntil tre av regissørens siste produksjoner kan gjøres tilgjengelig for påsyn")]
        public string InntilTre { get; set; }

        [DisplayName("Hvilke ? Skriv Tittel, År")]
        public string HvilkeSkrivTittelÅr { get; set; }

        [DisplayName("Følger det pilot eller annet visuelt materiale som blir ettersendt søknaden:")]
        public string Følgerdet { get; set; }

        public List<NettadresseVisueltMateriale> NettadresseVisueltMateriale { get; } = new List<NettadresseVisueltMateriale>();
    }
}