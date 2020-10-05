using PatchFm.Interfaces;
using PatchFm.LastFm.Net.Models;
using PatchFm.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchFm.Services
{
    public class AlbumTransformationService : IAlbumTransformationService
    {
        public Task<IEnumerable<Transformation>> GenerateAlbumTransformations(IEnumerable<Track> tracks)
        {
            IList<Transformation> transformations = new List<Transformation>();

            var groupedAlbums = tracks
                .GroupBy(track => track.Album)
                .ToList();

            var tracksByTitle = tracks
                .Select(track => track.Title)
                .Distinct()
                .ToList();

            foreach (var title in tracksByTitle)
            {
                var allAlbums = groupedAlbums.Where(album => album.Any(track => track.Title == title));

                if (allAlbums.Count() > 1)
                {
                    var albumWithMostPlays = allAlbums.Aggregate(allAlbums.First(), (currentAlbumWithMostPlays, currentAlbum) =>
                    {
                        if (string.IsNullOrEmpty(currentAlbumWithMostPlays.Key))
                        {
                            return currentAlbum;
                        }

                        if (string.IsNullOrEmpty(currentAlbum.Key))
                        {
                            return currentAlbumWithMostPlays;
                        }

                        return currentAlbum.Count() >= currentAlbumWithMostPlays.Count()
                            ? currentAlbum
                            : currentAlbumWithMostPlays;
                    });

                    transformations.Add(new Transformation()
                    {
                        Track = new Track()
                        {
                            Title = title,
                            Album = albumWithMostPlays.Key
                        },
                        TransformedTo = albumWithMostPlays.Key
                    });
                }
            }

            return Task.FromResult(transformations.AsEnumerable());
        }
    }
}