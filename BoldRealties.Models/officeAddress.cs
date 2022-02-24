using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BoldRealties.Models
{
    public class officeAddress
    {
        [Key]
        public int ID { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Phone1 { get; set; }
        public int Phone2 { get; set; }
        public int Fax { get; set; }
        public string MapPathLarge { get; set; }
        public string MapPathSmall { get; set; }
    }
}
