using PatchFm.LastFm.Net.Models;
using PatchFm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchFm.Interfaces
{
    public interface IAlbumTransformationService
    {
        Task<IEnumerable<Transformation>> GenerateAlbumTransformations(IEnumerable<Track> tracks);
    }
}