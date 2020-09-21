using System.Collections.Generic;

namespace PatchFm.LastFm.Net.Models
{
    public class GetUserTopTracksResult
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<Track> Tracks { get; set; } = new List<Track>();
    }
}