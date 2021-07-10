using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.Services;

namespace Twitcher.Controllers.JsonHelper
{
    public class JsonHelper
    {
        private string _savePath;
        public JsonHelper(string savePath)
        {
            if(!Directory.Exists(savePath))
                throw new DirectoryNotFoundException();

            _savePath = savePath;
        }
        public T GetObject<T>(string command, string selector)
        {
            string path = _savePath + command + ".json";

            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                JObject o = JObject.Parse(File.ReadAllText(path));
                JContainer container = (JContainer)o.SelectToken(selector);
                if (container == default)
                {
                    o.Add(selector, string.Empty);
                    File.WriteAllText(path, o.ToString());
                    return default;
                }
                return container.ToObject<T>();
            }

            File.AppendAllText(path, "{}");

            return default;
        }
        public void EditObject<T>(string command, string selector, T newInfo)
        {
            string path = _savePath + command + ".json";
            if (!File.Exists(path))
                File.WriteAllText(path, "{}");
            string text = File.ReadAllText(path);

            JObject o = JObject.Parse(File.ReadAllText(path));
            JContainer container = (JContainer)o.SelectToken(selector);
            if (container != null)
                o.Remove(selector);

            o.Add(selector, JContainer.FromObject(newInfo));
            File.WriteAllText(path, o.ToString());
        }
        
    }
}