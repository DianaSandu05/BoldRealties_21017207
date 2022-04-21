using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models.DocuSign.Models
{
    public interface IRequestItemsService
    {
        string EgName { get; set; }

        Session Session { get; set; }

        Users User { get; set; }
        public Guid? OrganizationId { get; set; }
        string EnvelopeId { get; set; }
        string DocumentId { get; set; }
        string ClickwrapId { get; set; }
        string ClickwrapName { get; set; }
        EnvelopeDocuments EnvelopeDocuments { get; set; }
        string TemplateId { get; set; }
        string PausedEnvelopeId { get; set; }
        string Status { get; set; }

        public void UpdateUserFromJWT();
        public void Logout();
        public bool CheckToken(int bufferMin);
    }
}