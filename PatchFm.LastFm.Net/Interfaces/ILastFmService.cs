using PatchFm.LastFm.Net.Models;
using System;
using System.Threading.Tasks;

namespace PatchFm.LastFm.Net.Interfaces
{
    public interface ILastFmService
    {
        Task<GetUserTopTracksResult> GetUserTopTracks(DateTime from, DateTime to, int page);
    }
}