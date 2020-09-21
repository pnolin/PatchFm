using PatchFm.LastFm.Net.Models;

namespace PatchFm.Models
{
    public class Transformation
    {
        public Track Track { get; set; } = new Track();
        public string TransformedTo { get; set; } = "";
    }
}