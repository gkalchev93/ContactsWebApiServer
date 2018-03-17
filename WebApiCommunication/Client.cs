using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiCommunication.DTO;

namespace WebApiCommunication
{
    public class Client
    {
        private HttpClient httpClient;
        public Client(string path)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(path);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Contact> GetContact(int id)
        {
            Contact tmpObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"/api/contacts/1");
            if(response.IsSuccessStatusCode)
            {
                var k = response.Content.ReadAsStringAsync();
                tmpObj = await response.Content.ReadAsAsync<Contact>();
            }

            return tmpObj;
        }
    }
}
