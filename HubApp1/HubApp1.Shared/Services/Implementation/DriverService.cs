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

    public class DriverService : IDriverService
    {
        private const string RestServiceUrl = "http://localhost/kartingapi/api/driver";

        //public async Task<Driver> AddDriver(Driver driver)
        //{
        //    var client = new HttpClient
        //    {
        //        BaseAddress = new Uri(RestServiceUrl)
        //    };


        //    return driver;
        //}

        public async Task<string> AddDriver(Driver driver)
        {
            string data = JsonConvert.SerializeObject(driver);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RestServiceUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            var stream = await request.GetRequestStreamAsync();

            //const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
            //var jsonSerializerSettings = new DataContractJsonSerializerSettings
            //{
            //    DateTimeFormat = new DateTimeFormat(dateTimeFormat)
            //};

            //var jsonSerializer = new DataContractJsonSerializer(
            //    typeof(Driver[]),
            //    jsonSerializerSettings);

            //jsonSerializer.WriteObject(stream, driver);

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

        public async Task<IEnumerable<Driver>> GetAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RestServiceUrl)
            };

            //// I want to add and accept a header for JSON
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //// Retrieve all the groups with their items
            var response = await client.GetAsync("driver");

            //// Throw an exception if something went wrong
            response.EnsureSuccessStatusCode();

            //// In case you need date and time properties
            const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
            var jsonSerializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat(dateTimeFormat)
            };

            var jsonSerializer = new DataContractJsonSerializer(
                typeof(Driver[]),
                jsonSerializerSettings);

            var stream = await response.Content.ReadAsStreamAsync();
            return (Driver[])jsonSerializer.ReadObject(stream);
        }
    }
}
