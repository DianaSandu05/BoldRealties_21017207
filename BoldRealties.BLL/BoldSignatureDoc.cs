using BoldSign.Api;
using BoldSign.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoldRealties.BLL
{
    public class ApiSignature
    {
     /*   private readonly HttpClient httpClient;
        private readonly ApiClient apiClient;
        private static string accessToken = null;
        private static DateTime? expiresAt = null;

        public ApiSignature(HttpClient httpClient, ApiClient apiClient)
        {
            this.httpClient = httpClient;
            this.apiClient = apiClient;
        }
*/
    /*    public async Task SetTokenAsync()
        {
            if (!(accessToken != null && expiresAt != null && expiresAt > DateTime.UtcNow.AddMinutes(-5)))
            {
                // Need to add required scopes.
                var parameters = new List<KeyValuePair<string, string>>
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("scope", "BoldSign.Documents.All BoldSign.Templates.All")
            };

                using var encodedContent = new FormUrlEncodedContent(parameters);

                using var request = new HttpRequestMessage()
                {
                    Content = encodedContent,
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://account.boldsign.com/connect/token"),
                };

                //Clientid for get access token
                const string clientId = "0a6aa257-9810-4305-a6e0-4bbfebff3e7f";

                //Clientsecret for get access token
                const string clientSecret = "831bc481-718a-4c0e-9860-419c91f7809a";

                //if doesnot work, uncomment
                *//*  var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                  request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);*//*
                var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);

                //Send request for fetch access token
                using var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                tokenResponse.TryGetValue("access_token", out accessToken);
                tokenResponse.TryGetValue("expires_in", out var expiresIn);
                expiresAt = DateTime.UtcNow.AddSeconds(Convert.ToInt32(expiresIn));
                this.apiClient.Configuration.DefaultHeader.Remove("Authorization");
                // Added accesstoken in default header.
                this.apiClient.Configuration.DefaultHeader.TryAdd("Authorization", "Bearer " + accessToken);



            }
        }
*/
        /*    //I might move it

           public async Task TokenConfigs()
            {
                using var http = new HttpClient();
                string accessToken = null;
                // need to add required scopes.
                var parameters = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("scope", "BoldSign.Documents.All BoldSign.Templates.All")
          };

                using var encodedContent = new FormUrlEncodedContent(parameters);
                using var request = new HttpRequestMessage()
                {
                    Content = encodedContent,
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://account.boldsign.com/connect/token"),
                };

                //clientid for get access token
                const string clientId = "0a6aa257-9810-4305-a6e0-4bbfebff3e7f";

                //clientsecret for get access token
                const string clientSecret = "831bc481-718a-4c0e-9860-419c91f7809a";

                var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);

                //send request for fetch access token
                using var response = await this.httpClient.SendAsync(request).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                tokenResponse.TryGetValue("access_token", out accessToken);
                var configuration = new Configuration();

                // set your OAuth2 Access Token for authentication.
                configuration.SetBearerToken(accessToken);
                var apiClient = new ApiClient(configuration);
                var documentClient = new DocumentClient(apiClient);
            }*/
        
         /*   public async Task CreateDocument()
            {
                // Use the template ID copied from the options.
                var templateId = "367f0d1b-ccbe-401a-91f0-6d6555a0a8c4";

                // Fill the existing form fields value by name.

                // Add the existing filled form fields to a signer role.
                var roles = new List<Roles>
        {
            new Roles
            {
        RoleIndex = 1,
        SignerEmail = "signer1@email.com",
        SignerName = "Signer Name",

    },
};

                var templateDetails = new SendForSignFromTemplate(
                    templateId: templateId,
                    title: "BoldRealties",
                    message: "Test",
                    roles: roles);

                // Send the template for sign request.


                var templateClient = new TemplateClient(apiClient);
                var documentCreated = templateClient.SendUsingTemplate(templateDetails);

                // Send the document for signing.

            }
*/
        }

    }
