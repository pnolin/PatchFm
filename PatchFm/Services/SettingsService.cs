using PatchFm.Interfaces;
using PatchFm.Models;
using System;

namespace PatchFm.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly Settings _settings;

        public SettingsService(Settings settings)
        {
            _settings = settings;
        }

        public DateTime? ScrobblingSince => _settings.StartDate;
        public string LastFmUsername => _settings.LastFmUsername;
        public string LastFmApiKey => _settings.LastFmApiKey;
        public string LastFmApiSecret => _settings.LastFmApiSecret;
    }
}