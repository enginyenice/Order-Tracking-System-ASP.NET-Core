using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Models.Tables
{
    public class Department
    {
        [Key]
        public int depId { get; set; }
        public string depName { get; set; }
    }
}
