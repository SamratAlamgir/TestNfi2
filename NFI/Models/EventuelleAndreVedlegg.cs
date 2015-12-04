using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    [DisplayName("Eventuelle andre vedlegg")]
    public class EventuelleAndreVedlegg : IMember
    {

        [DisplayName(" Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:"), FileSize, JsonIgnore]
        public List<HttpPostedFileBase> Harduvedlegg { get; set; }
        [DisplayName(" Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg de ved her:")]
        public List<string> HarduvedleggPaths { get; set; }

        [DisplayName(" Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Andre vedlegg.")]
        public string Beskrivelseav { get; set; }
    }
}