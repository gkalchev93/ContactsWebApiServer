﻿using System;
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
            httpClient.Timeout = new TimeSpan(0, 0, 30);
            httpClient.BaseAddress = new Uri(path);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Contact[]> GetContacts()
        {
            Contact[] retObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"api/Contacts").ConfigureAwait(false);
            if(response.IsSuccessStatusCode)
            {
                retObj = await response.Content.ReadAsAsync<Contact[]>();
            }

            return retObj;
        }

        public void CreateContact(Contact item)
        {
            var createTask = httpClient.PostAsJsonAsync($"api/Contacts", item);
            createTask.Wait();
            HttpResponseMessage response = createTask.Result;
            response.EnsureSuccessStatusCode();
        }

        public void UpdateContact(Contact item)
        {
            var putTask = httpClient.PutAsJsonAsync($"api/Contacts/{item.Id}", item);
            putTask.Wait();
            HttpResponseMessage response = putTask.Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to update.");
        }

        public void DeleteContact(int id)
        {
            var deleteTask = httpClient.DeleteAsync($"api/Contacts/{id}");
            deleteTask.Wait();
            HttpResponseMessage response = deleteTask.Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task<Contact[]> GetContactsByName(string name, bool contains)
        {
            Contact[] retObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"api/Contacts?name={name}&contains={contains}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                retObj = await response.Content.ReadAsAsync<Contact[]>();
            }

            return retObj;
        }

        public async Task<Contact[]> GetContactsByEgn(string egn, bool contains)
        {
            Contact[] retObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"api/Contacts?egn={egn}&contains={contains}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                retObj = await response.Content.ReadAsAsync<Contact[]>();
            }

            return retObj;
        }

        public async Task<Contact[]> GetContactsByAddress(string adr, bool contains)
        {
            Contact[] retObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"api/Contacts?adr={adr}&contains={contains}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                retObj = await response.Content.ReadAsAsync<Contact[]>();
            }

            return retObj;
        }

        public async Task<Contact[]> GetContactsByTelephone(string tel, bool contains)
        {
            Contact[] retObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"api/Contacts?tel={tel}&contains={contains}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                retObj = await response.Content.ReadAsAsync<Contact[]>();
            }

            return retObj;
        }

        public async Task<Contact[]> GetContactsById(int id, bool contains)
        {
            Contact[] retObj = null;

            HttpResponseMessage response = await httpClient.GetAsync($"api/Contacts/{id}?contains={contains}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                retObj = await response.Content.ReadAsAsync<Contact[]>();
            }

            return retObj;
        }
    }
}
