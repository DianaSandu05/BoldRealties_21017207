using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldRealties.Models
{
    public class StripeSettings
    {
        //these properties are used to retrieve the values from appsettings.json
       // the mapping is done in the file program.cs
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
