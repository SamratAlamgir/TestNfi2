using System.ComponentModel;

namespace NFI.Models
{
    [DisplayName("Eventuelle andre vedlegg")]
    public class EventuelleAndreVedlegg : IMember
    {
        [Header("Eventuelle andre vedlegg")]
        [DisplayName(" Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:")]
        public string Harduvedlegg { get; set; }

        [DisplayName(" Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Andre vedlegg.")]
        public string Beskrivelseav { get; set; }
    }
}