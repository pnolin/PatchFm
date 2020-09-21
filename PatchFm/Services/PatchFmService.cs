using PatchFm.Interfaces;
using PatchFm.LastFm.Net.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PatchFm.Services
{
    public class PatchFmService : IPatchFmService
    {
        private const string DirectoryPath = "./patches";

        private readonly ILastFmService _lastFmService;
        private readonly IAlbumTransformationService _albumTransformationService;
        private readonly ISettingsService _settingsService;

        private DateTime _startDate;

        public PatchFmService(
            ILastFmService lastFmService,
            IAlbumTransformationService albumTransformationService,
            ISettingsService settingsService)
        {
            _lastFmService = lastFmService;
            _albumTransformationService = albumTransformationService;
            _settingsService = settingsService;

            _startDate = _settingsService.ScrobblingSince ?? DateTime.Now.AddDays(-1);
        }

        public async Task PatchFm()
        {
            Directory.CreateDirectory(DirectoryPath);

            var firstPage = await _lastFmService.GetUserTopTracks(_startDate, DateTime.Now, 1);
            var tracks = firstPage.Tracks;

            if (firstPage.TotalPage > 1)
            {
                tracks = tracks.Concat(
                    Enumerable
                        .Range(2, firstPage.TotalPage)
                        .Select(page => _lastFmService.GetUserTopTracks(_startDate, DateTime.Now, page))
                        .SelectMany(task => task.Result.Tracks));
            }

            var albumTransformations = await _albumTransformationService.GenerateAlbumTransformations(tracks);
            var albumTransformationsAsText = albumTransformations
                .Select(transformation => $"{transformation.Track.Title} should have its album changed from " +
                $"{transformation.Track.Album} to {transformation.TransformedTo}");

            var date = DateTime.Now.ToString("yyyy-MM-dd");

            File.WriteAllLines($"{DirectoryPath}/{date}-patch-fm.txt", albumTransformationsAsText);
        }
    }
}