using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;

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

        public static object UpdateObject(object original, object updated, string identityFieldName)
        {
            try
            {
                PropertyInfo[] newProperties = updated.GetType().GetProperties();
                Dictionary<string, PropertyInfo> dict = new Dictionary<string, PropertyInfo>();
                foreach (PropertyInfo p in newProperties)
                {
                    try
                    {
                        dict.Add(p.Name, p);
                    }
                    catch { }
                }
                PropertyInfo[] properties = original.GetType().GetProperties();
                foreach (PropertyInfo prop in properties)
                {
                    try
                    {
                        object value = dict[prop.Name].GetValue(updated, null);
                        if (prop.Name.ToLower() != identityFieldName.ToLower() && value != null && !prop.PropertyType.FullName.Contains("System.Data.Linq.EntitySet"))
                        {
                            try
                            {
                                prop.SetValue(original, value, null);
                            }
                            catch
                            {
                                // Property does not have a set value
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return original;
        }
    }
}