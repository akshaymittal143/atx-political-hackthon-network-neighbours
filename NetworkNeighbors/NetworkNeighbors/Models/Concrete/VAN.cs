using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NetworkNeighbors.Models.Abstract;

namespace NetworkNeighbors.Models.Concrete
{
    public partial class Repository: IDataRepository
    {
        #region VAN
        private string VANAPIKey
        {
            get
            {
                return Helpers.GetConfig("VanApiKey", "38dd0ae3-fb26-d842-751e-92ce6f18a7b5");
            }
        }

        private string VANAPIUsername
        {
            get
            {
                return Helpers.GetConfig("VanApiUsername", "38dd0ae3-fb26-d842-751e-92ce6f18a7b5");
            }
        }

        private string VANAPIBaseUrl
        {
            get
            {
                return Helpers.GetConfig("VanApiBaseUrl", "https://api.securevan.com/v4");
            }
        }

        private string VANAPIRequest(string path, string method = "GET", object data = null, string dbMode = "0")
        {
            try
            {
                string url = VANAPIBaseUrl + path;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = method;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", VANAPIUsername, VANAPIKey + "|" + dbMode))));
                if (request.Method == "POST" && data != null)
                {
                    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                    request.ContentType = "application/json";
                    request.ContentLength = postBytes.Length;
                    var stream = request.GetRequestStream();
                    stream.Write(postBytes, 0, postBytes.Length);
                    stream.Close();
                }
                try
                {
                    WebResponse response = request.GetResponse();
                    using (var resStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(resStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var read = new StreamReader(stream))
                    {
                        string str = read.ReadToEnd();
                        HttpContext.Current.Trace.Write(ex.ToString());
                        return str;
                    }
                }
            }
            catch(Exception ex)
            {
                HttpContext.Current.Trace.Warn(ex.ToString());
            }
            return String.Empty;
        }

        public string VANEcho(string str)
        {
            return VANAPIRequest("/echoes", "POST", new { message = str });
        }

        #endregion
    }
}