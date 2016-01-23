using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubApp1.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;

    using System.Threading.Tasks;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using HubApp1.Entities;
    using HubApp1.Services;

    using System.Net;
    using System.IO;
    using Newtonsoft.Json;

    public class RaceService : IRaceService
    {
        private const string RestServiceUrl = "http://race-tracker.azurewebsites.net/api/races";

        public async Task<string> DeleteRace(int id)
        {
            string data = id.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RestServiceUrl + "/" + id.ToString());
            request.ContentType = "application/json";
            request.Method = "DELETE";
            var stream = await request.GetRequestStreamAsync();

            using (var writer = new StreamWriter(stream))
            {
                writer.Write(data);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();

            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                return sr.ReadToEnd();
            }
        }

        public async Task<string> UpdateRace(Race race)
        {
            string data = JsonConvert.SerializeObject(race);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RestServiceUrl + "/" + race.ID.ToString());
            request.ContentType = "application/json";
            request.Method = "PUT";
            var stream = await request.GetRequestStreamAsync();

            using (var writer = new StreamWriter(stream))
            {
                writer.Write(data);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();


            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                return sr.ReadToEnd();
            }
        }

        public async Task<string> AddRace(Race race)
        {
            string data = JsonConvert.SerializeObject(race);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RestServiceUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            var stream = await request.GetRequestStreamAsync();

            using (var writer = new StreamWriter(stream))
            {
                writer.Write(data);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();

            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                return sr.ReadToEnd();
            }
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RestServiceUrl)
            };

            //// I want to add and accept a header for JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //// Retrieve all the groups with their items
            var response = await client.GetAsync("races");

            //// Throw an exception if something went wrong
            response.EnsureSuccessStatusCode();

            //// In case you need date and time properties
            const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
            var jsonSerializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat(dateTimeFormat)
            };

            var jsonSerializer = new DataContractJsonSerializer(
                typeof(Race[]),
                jsonSerializerSettings);

            var stream = await response.Content.ReadAsStreamAsync();
            return (Race[])jsonSerializer.ReadObject(stream);
        }
    }
}
