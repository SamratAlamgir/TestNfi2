using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFI.Models
{
    public class DacListen:IMember
    {
        [Required]
        [Header("DAC-listen")]
        [DisplayName("Hovedprodusentens foretak er etablert i et land p� DAC-listen: ")]
        public string HovedprodusentensForetak { get; set; }

        [Required]
        [DisplayName("Hvilket ? ")]
        public string Hvilket { get; set; }

        [Required]
        [DisplayName(" Regiss�ren er statsborger av eller er bosatt i et land p� DAC-listen: ")]
        public string Regiss�renErStatsborger { get; set; }

        [Required]
        [DisplayName("Hvilket land ?")]
        public string Hvilketland { get; set; }

        [Required]
        [DisplayName("Filmen skal i vesentlig grad spilles i inn i land p� DAC-listen?")]
        public string FilmenSka { get; set; }

        [Required]
        [DisplayName("Hvilket/hvilke land og hvor mye: Skriv Land, xx%")]
        public string HvilkethvilkeLand { get; set; }

        [DisplayName("Eventuell kommentar til DAC-listen")]
        public string EventuellKommentar { get; set; }
    }
}