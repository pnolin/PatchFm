using IF.Lastfm.Core.Api;
using PatchFm.LastFm.Net.Interfaces;
using PatchFm.LastFm.Net.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PatchFm.LastFm.Net.Services
{
    public class LastFmService : ILastFmService
    {
        private readonly LastfmClient _client;
        private readonly string _username;

        public LastFmService(string username, string apiKey, string apiSecret)
        {
            _client = new LastfmClient(apiKey, apiSecret);
            _username = username;
        }

        public async Task<GetUserTopTracksResult> GetUserTopTracks(DateTime from, DateTime to, int page)
        {
            DateTimeOffset fromDate = new DateTimeOffset(from);
            DateTimeOffset toDate = new DateTimeOffset(to);

            var response = await _client.User.GetRecentScrobbles(_username, fromDate, toDate, false, page, 200);

            return new GetUserTopTracksResult()
            {
                CurrentPage = response.Page,
                TotalPage = response.TotalPages,
                Tracks = response.Content.Select(track => new Track()
                {
                    Title = track.Name,
                    Artist = track.ArtistName,
                    Album = track.AlbumName
                })
            };
        }
    }
}