using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FlashcardsCourseProject.Services.API
{
    public class AuthService
    {
        public HttpClient CreateHttpClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.BaseAddress = new Uri("");
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue(_options.Value.AcceptHeader));
            //string? authHeaders = $"Bearer {_options.Value.BearerToken}, User {_options.Value.UserToken}";
            //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authHeaders);

            return httpClient;
        }
    }
}
