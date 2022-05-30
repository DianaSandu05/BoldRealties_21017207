using BoldRealties.Models;
using BoldSign.Api;
using BoldSign.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace BoldRealties.Web.Controllers
{
   
    [Route("Doc")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly ApiClient apiClient;
        private static string accessToken = null;
        private static DateTime? expiresAt = null;
        public string DocumentID { get; set; }
        /*   private readonly ILogger<BoldApiController> _logger;
      *//*     private readonly TemplateClient templateClient;*//*
           private readonly DocumentClient documentClient;*/
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("BoldSign", new AuthenticationProperties()
            {
                RedirectUri = returnUrl
            });
        }
        public ApiController(HttpClient httpClient, ApiClient apiClient
           /* ILogger<BoldApiController> logger, TemplateClient templateClient, DocumentClient documentClient*/)
        {

          /*  _logger = logger;
            this.documentClient = documentClient;
           *//* this.templateClient = templateClient;*/
            this.httpClient = httpClient;
            this.apiClient = apiClient;
        }
        public async Task SetTokenAsync()
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
                var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);
                /*  var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
                  request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);*/

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

                var configuration = new Configuration();
                configuration.SetBearerToken(accessToken);
                // set your OAuth2 Access Token for authentication.

                var apiClient = new ApiClient(configuration);

            }
        }

        /*   [HttpGet]
           [ActionName("SignLink")]
           public async Task<EmbeddedSignDetails> SignLink()
           {
               var templateClient = new TemplateClient(apiClient);
               var sendForSignFromTemplate = new SendForSignFromTemplate()
               {
                   TemplateId = "367f0d1b-ccbe-401a-91f0-6d6555a0a8c4",
                   Title = "BoldRealties",
               };
               DocumentCreated documentCreated = await templateClient.SendUsingTemplateAsync(sendForSignFromTemplate).ConfigureAwait(false);
               EmbeddedSigningLink embeddedSigning = this.documentClient.GetEmbeddedSignLink(
                    documentId: documentCreated.DocumentId,
                    signerEmail: "Signer1@email.com",
                    redirectUrl: $"{this.Request.Scheme}://{this.Request.Host}/response");
               return new EmbeddedSignDetails() { DocumentID = documentCreated.DocumentId, SignLink = embeddedSigning.SignLink };
           }
   */
    }
}

      /*  [HttpPost]
        public IActionResult CreateDocument()
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
          
           
        }*/
