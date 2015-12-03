using System.ComponentModel;

namespace NFI.Models
{
    public class NettadresseVisueltMateriale:IMember
    {
        [DisplayName("Nettadresse til eventuelt visuelt materiale(webside, Vimeo, etc):")]
        public string Nettadressetil { get; set; }

        [DisplayName("Eventuelt passord for adgang til materialet:")]
        public string Eventueltpassord { get; set; }
    }
}