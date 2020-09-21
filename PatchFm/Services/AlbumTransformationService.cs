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
            var dictionnary = tracks
                .Aggregate(new Dictionary<string, List<Track>>(), (dictionnary, currentTrack) =>
                {
                    if (dictionnary.ContainsKey(currentTrack.Album))
                    {
                        dictionnary[currentTrack.Album].Add(currentTrack);
                        return dictionnary;
                    }

                    dictionnary.Add(currentTrack.Album, new List<Track>() { currentTrack });

                    return dictionnary;
                });

            var albumTransformations = tracks
                .Select(track =>
                {
                    var currentAlbumCount = dictionnary[track.Album].Count();
                    var potentialAlbumList = dictionnary
                        .Where(item => item.Value.Any(albumTrack =>
                        {
                            return albumTrack.Title == track.Title &&
                                albumTrack.Artist == track.Artist &&
                                albumTrack.Album != track.Album;
                        }));

                    var potentialAlbum = potentialAlbumList.Count() == 0
                        ? track.Album
                        : potentialAlbumList.Aggregate(track.Album, (currentPotentialAlbum, currentKeyValuePair) =>
                            currentPotentialAlbum != string.Empty &&
                            dictionnary[currentPotentialAlbum].Count() >= currentKeyValuePair.Value.Count()
                                ? currentPotentialAlbum
                                : currentKeyValuePair.Key);

                    return new Transformation() { Track = track, TransformedTo = potentialAlbum };
                })
                .Where(transformation => transformation.Track.Album != transformation.TransformedTo);

            return Task.FromResult(albumTransformations);
        }
    }
}