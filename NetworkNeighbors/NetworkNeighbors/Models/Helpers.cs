using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace NetworkNeighbors.Models
{
    public class Helpers
    {
        public static string GetConfig(string key, string defaultValue = "")
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                return ConfigurationManager.AppSettings[key];
            }
            else
            {
                return defaultValue;
            }
        }
    }
}