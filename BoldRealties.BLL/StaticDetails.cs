using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.BLL
{
    public class StaticDetails
    {
        public const string Role_User = "Employee";
        public const string Role_Tenant = "Tenant";
        public const string Role_Landlord = "Landlord";
        public const string Role_Subcontractor = "Subcontractor";
        public const string Role_Admin = "Admin";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        public const string SessionPayment = "SessionPayment";


        public static IEnumerable<IdentityResource> IdentityResources =>
         new List<IdentityResource>
         {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
         };
        public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope> {
                new ApiScope("DocuSign", "DocuSign"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
             };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "da69d2fd-8a45-42b4-970e-112602fa59a1",
                    ClientSecrets = {new Secret("a8a9cdb7-463f-4bdc-8bc2-026157a16e9e".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "profile"},
                    RedirectUris = {"https://account-d.docusign.com/oauth/auth"},
                    PostLogoutRedirectUris = {"https://localhost:7208//ds/callback"},
                   
                },
                new Client
                {
                    ClientId = "DocuSign:ClientId",
                    ClientSecrets = {new Secret("DocuSign:ClientSecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://account-d.docusign.com/oauth/auth"},
                    PostLogoutRedirectUris = {"https://localhost:7208//ds/callback"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "DocuSign"
                    }
                }
            };
    }
}
