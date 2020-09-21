﻿using System;

namespace PatchFm.Models
{
    public class Settings
    {
        public DateTime? ScrobblingSince { get; set; }
        public string LastFmUsername { get; set; } = "";
        public string LastFmApiKey { get; set; } = "";
        public string LastFmApiSecret { get; set; } = "";
    }
}