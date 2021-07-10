using System;
using System.Collections.Generic;
using Twitcher.Controllers.Services;

namespace Twitcher.Controllers.JsonHelper
{
    public class JsonSettings
    {
        public string SavePath { get; }

        public JsonSettings(string savePath)
        {
            SavePath = savePath + '/';
        }
    }
}