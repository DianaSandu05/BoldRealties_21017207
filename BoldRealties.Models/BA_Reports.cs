using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BoldRealties.Models
{
    public class BA_Reports
    {
        [Key]
        public int ID { get; set; }
        public string FilePath { get; set; }
        public string Report_Name { get; set; }

        public int tenancyID { get; set; }
        [ForeignKey("tenancyID")]
        [ValidateNever]
        public tenancies tenancies { get; set; }

        public string Outcome { get; set; }
    }
}
