using System.Collections.Generic;
using System.ComponentModel;

namespace NFI.Models
{
    public class VisueltMateriale:IMember
    {
        [Header("Visuelt materiale")]
        [DisplayName("Inntil tre av regiss�rens siste produksjoner kan gj�res tilgjengelig for p�syn")]
        public string InntilTre { get; set; }

        [DisplayName("Hvilke ? Skriv Tittel, �r, Linjeskift")]
        public string HvilkeSkrivTittel�r { get; set; }

        [DisplayName("F�lger det pilot eller annet visuelt materiale som blir ettersendt s�knaden:")]
        public string F�lgerdet { get; set; }
        [DisplayName(" Nett adresse Visuelt Materiales:")]
        public List<NettadresseVisueltMateriale> NettadresseVisueltMateriale { get; } = new List<NettadresseVisueltMateriale>();
    }
}