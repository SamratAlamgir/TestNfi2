using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    [DisplayName("Eventuelle andre vedlegg")]
    public class EventuelleAndreVedlegg : IMember
    {

        [DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg ved her (marker alle vedleggene du vil laste opp i en prosess):"), FileSize, JsonIgnore]
        public List<HttpPostedFileBase> Harduvedlegg { get; set; }
        [DisplayName("Har du vedlegg som er relevante til søknad som du ikke har fått lastet opp? Legg ved her (marker alle vedleggene du vil laste opp i en prosess):")]
        public List<string> HarduvedleggPaths { get; set; }

        [DisplayName("Beskrivelse av andre vedlegg: Beskriv innholdet i vedleggene lastet opp under Andre vedlegg.")]
        public string Beskrivelseav { get; set; }
    }
}