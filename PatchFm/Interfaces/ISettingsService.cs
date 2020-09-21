using System;

namespace PatchFm.Interfaces
{
    public interface ISettingsService
    {
        public DateTime? ScrobblingSince { get; }
        public string LastFmUsername { get; }
        public string LastFmApiKey { get; }
        public string LastFmApiSecret { get; }
    }
}