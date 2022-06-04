using FlashcardsCourseProject.Services.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services.API
{
    public class RestAPIService
    {
        private AuthService _authService => DependencyService.Get<AuthService>();

        public async Task<T> Get<T>(string url, object body)
        {

                HttpClient httpClient = _authService.CreateHttpClient();

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "uriApi" + url);
                //message.Headers.Add("Accept", _options.Value.AcceptHeader);

                if (body != null)
                {
                    JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    };

                    string requestData = JsonConvert.SerializeObject(body, serializerSettings);

                    message.Content = new StringContent(requestData);
                    message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                HttpResponseMessage response = await httpClient.SendAsync(message);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Response<T> data = JsonConvert.DeserializeObject<Response<T>>(responseContent);

                    if (data == null) return default;
                    T model = data.Data;

                    return model;
                }
                
            return default;
        }

        public async Task<T> Post<T>(string url, object body)
        {

            HttpClient httpClient = _authService.CreateHttpClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "uriApi" + url);
            //message.Headers.Add("Accept", _options.Value.AcceptHeader);

            if (body != null)
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };

                string requestData = JsonConvert.SerializeObject(body, serializerSettings);

                message.Content = new StringContent(requestData);
                message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(message);

            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Response<T> data = JsonConvert.DeserializeObject<Response<T>>(responseContent);

                if (data == null) return default;
                T model = data.Data;

                return model;
            }

            return default;
        }

        public async Task<Response<T>> PostAdd<T>(string url, object body)
        {

            HttpClient httpClient = _authService.CreateHttpClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "uriApi" + url);
            //message.Headers.Add("Accept", _options.Value.AcceptHeader);

            if (body != null)
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };

                string requestData = JsonConvert.SerializeObject(body, serializerSettings);

                message.Content = new StringContent(requestData);
                message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(message);

            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Response<T> data = JsonConvert.DeserializeObject<Response<T>>(responseContent);

                //if (data == null) return default;
                //T model = data.Data;

                return data;
            }

            return default;
        }


        public async Task<Response<T>> Put<T>(string url, object body)
        {

            HttpClient httpClient = _authService.CreateHttpClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, "uriApi" + url);
            //message.Headers.Add("Accept", _options.Value.AcceptHeader);

            if (body != null)
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };

                string requestData = JsonConvert.SerializeObject(body, serializerSettings);

                message.Content = new StringContent(requestData);
                message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(message);

            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Response<T> data = JsonConvert.DeserializeObject<Response<T>>(responseContent);

                //if (data == null) return default;
                //T model = data.Data;

                return data;
            }

            return default;
        }

        public async Task<Response<T>> Delete<T>(string url)
        {
            HttpClient httpClient = _authService.CreateHttpClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "uriApi" + url);
            //message.Headers.Add("Accept", _options.Value.AcceptHeader);

            HttpResponseMessage response = await httpClient.SendAsync(message);

            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Response<T> data = JsonConvert.DeserializeObject<Response<T>>(responseContent);

                //if (data == null) return default;
                //T model = data.Data;

                return data;
            }

            return default;
        }
    }
}
