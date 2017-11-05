using System;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NetworkNeighbors.DTOs.VAN;
using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace NetworkNeighbors.Models.Concrete
{
    public partial class Repository: IDataRepository
    {
        #region VAN

        /// <summary>
        /// Sends a message to VAN that is returned.
        /// </summary>
        /// <param name="str">Message to send</param>
        /// <returns>Message received from VAN</returns>
        public Task<string> VANEchoAsync(string str)
        {
            return VANAPIRequestAsync("echoes", HttpMethod.Post, new { message = str });
        }

        /// <summary>
        /// Checks the VAN to see if a certain person is registered; if not, they are added to it.
        /// </summary>
        /// <param name="person">Person to find or create</param>
        /// <returns>Whether the person was found in the VAN</returns>
        public async Task<MatchResponse> FindOrCreatePersonInVANAsync(PersonDTO person)
        {
            MatchResponse response = new MatchResponse();
            const string path = "people/findOrCreate";

            response = JsonConvert.DeserializeObject<MatchResponse>(await VANAPIRequestAsync(path, HttpMethod.Post, person));

            return response;

        }

        /// <summary>
        /// Checks the VAN to see if a certain person is registered.
        /// </summary>
        /// <param name="person">Person to find</param>
        /// <returns>Whether the person was found in the VAN</returns>
        public async Task<MatchResponse> FindPersonInVANAsync(PersonDTO person)
        {
            MatchResponse response = new MatchResponse();
            const string path = "people/find";

            response = JsonConvert.DeserializeObject<MatchResponse>(await VANAPIRequestAsync(path, HttpMethod.Post, person));

            return response;

        }

        public async Task<Voter> GetPersonInVANAsync(string vanID)
        {
            try
            {
                string result = await VANAPIRequestAsync("people/" + vanID, HttpMethod.Get);
                JObject json = JObject.Parse(result);
                if(Helpers.SafeJsonString(json, "vanId") == vanID)
                {
                    var voter = new Voter
                    {
                        first_name = Helpers.SafeJsonString(json, "firstName"),
                        last_name = Helpers.SafeJsonString(json, "lastName")
                    };
                    JToken addressObj = json["addresses"].First;
                    if(addressObj != null)
                    {
                        voter.address_1 = Helpers.SafeJsonString(addressObj, "addressLine1");
                        voter.address_2 = Helpers.SafeJsonString(addressObj, "addressLine2");
                        voter.city = Helpers.SafeJsonString(addressObj, "city");
                        voter.state = Helpers.SafeJsonString(addressObj, "stateOrProvince");
                        voter.zip_code = Helpers.SafeJsonString(addressObj, "zipOrPostalCode");
                    }
                    return voter;
                }
            }
            catch(Exception ex)
            {
                HttpContext.Current.Trace.Warn(ex.ToString());
            }
            return null;
        }

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
                return Helpers.GetConfig("VanApiUsername", "api.atx.hack.team11");
            }
        }

        private string VANAPIBaseUrl
        {
            get
            {
                return Helpers.GetConfig("VanApiBaseUrl", "https://api.securevan.com/v4/");
            }
        }

        private async Task<string> VANAPIRequestAsync(string path, HttpMethod method, object data = null, string dbMode = "0")
        {
            try
            {
                string url = VANAPIBaseUrl + path;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = method.ToString();
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", VANAPIUsername, VANAPIKey + "|" + dbMode))));

                if (request.Method == "POST" && data != null)
                {
                    byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                    request.ContentType = "application/json";
                    request.ContentLength = postBytes.Length;
                    var stream = request.GetRequestStream();
                    stream.Write(postBytes, 0, postBytes.Length);
                    stream.Close();
                }

                try
                {
                    WebResponse response = await request.GetResponseAsync();
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

            return string.Empty;
        }

        #endregion
    }
}