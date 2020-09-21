using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatchFm.Interfaces;
using PatchFm.LastFm.Net.Interfaces;
using PatchFm.LastFm.Net.Services;
using PatchFm.Models;
using PatchFm.Services;

namespace PatchFm.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupDI(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new Settings();

            configuration.Bind(settings);

            var settingsService = new SettingsService(settings);
            var lastFmService = new LastFmService(settingsService.LastFmUsername, settingsService.LastFmApiKey, settingsService.LastFmApiSecret);

            services.AddSingleton<ILastFmService>(lastFmService);
            services.AddSingleton<IAlbumTransformationService, AlbumTransformationService>();
            services.AddSingleton<ISettingsService>(settingsService);

            services.AddSingleton<IPatchFmService, PatchFmService>();
        }
    }
}