using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public static string SafeJsonString(JToken obj, string key)
        {
            try
            {
                return (string)obj[key];
            }
            catch
            {
                try
                {
                    int val = (int)obj[key];
                    return val.ToString();
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Trace.Warn(key + ": " + ex.ToString());
                    return "";
                }
            }
        }

        public static List<SelectListItem> GetStateSelectList(string defaultValue = "", string emptyText = "--SELECT--")
        {
            return CreateList(StateDictionary, defaultValue, emptyText);
        }

        private static List<SelectListItem> CreateList(Dictionary<string, string> items, string defaultValue, string emptyTitle)
        {
            if (defaultValue == null || (defaultValue == "0"))
            {
                defaultValue = "";
            }
            List<SelectListItem> list = new List<SelectListItem>();
            if (!String.IsNullOrEmpty(emptyTitle))
            {
                if (emptyTitle == null)
                {
                    emptyTitle = String.Empty;
                }
                list.Add(new SelectListItem { Value = "", Text = emptyTitle });
            }
            foreach (string key in items.Keys)
            {
                list.Add(new SelectListItem { Text = items[key], Value = key, Selected = IsSelected(key, defaultValue) });
            }
            return list;
        }

        private static bool IsSelected(string itemValue, string defaultValue)
        {
            if (itemValue == defaultValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Dictionary<string, string> StateDictionary
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("AL", "Alabama");
                dict.Add("AK", "Alaska");
                dict.Add("AZ", "Arizona");
                dict.Add("AR", "Arkansas");
                dict.Add("CA", "California");
                dict.Add("CO", "Colorado");
                dict.Add("CT", "Connecticut");
                dict.Add("DE", "Delaware");
                dict.Add("DC", "District Of Columbia");
                dict.Add("FL", "Florida");
                dict.Add("GA", "Georgia");
                dict.Add("HI", "Hawaii");
                dict.Add("ID", "Idaho");
                dict.Add("IL", "Illinois");
                dict.Add("IN", "Indiana");
                dict.Add("IA", "Iowa");
                dict.Add("KS", "Kansas");
                dict.Add("KY", "Kentucky");
                dict.Add("LA", "Louisiana");
                dict.Add("ME", "Maine");
                dict.Add("MD", "Maryland");
                dict.Add("MA", "Massachusetts");
                dict.Add("MI", "Michigan");
                dict.Add("MN", "Minnesota");
                dict.Add("MS", "Mississippi");
                dict.Add("MO", "Missouri");
                dict.Add("MT", "Montana");
                dict.Add("NE", "Nebraska");
                dict.Add("NV", "Nevada");
                dict.Add("NH", "New Hampshire");
                dict.Add("NJ", "New Jersey");
                dict.Add("NM", "New Mexico");
                dict.Add("NY", "New York");
                dict.Add("NC", "North Carolina");
                dict.Add("ND", "North Dakota");
                dict.Add("OH", "Ohio");
                dict.Add("OK", "Oklahoma");
                dict.Add("OR", "Oregon");
                dict.Add("PA", "Pennsylvania");
                dict.Add("RI", "Rhode Island");
                dict.Add("SC", "South Carolina");
                dict.Add("SD", "South Dakota");
                dict.Add("TN", "Tennessee");
                dict.Add("TX", "Texas");
                dict.Add("UT", "Utah");
                dict.Add("VT", "Vermont");
                dict.Add("VA", "Virginia");
                dict.Add("WA", "Washington");
                dict.Add("WV", "West Virginia");
                dict.Add("WI", "Wisconsin");
                dict.Add("WY", "Wyoming");
                dict.Add("International", "International");
                return dict;
            }
        }
    }
}