﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BoldRealties.Models
{
    public class payment
    {
        [Key]
        public int ID{get; set; }
        public float amount { get; set; }
        public DateTime received_Date { get; set; }
        public int TenancyID { get; set; }
        [ForeignKey("TenancyID")]
        [ValidateNever]
        public tenancies tenancies { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public Users Users { get; set; }

        public string currency { get; set; }

    }
}