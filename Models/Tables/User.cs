using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        [Required]
        public string userMail { get; set; }
        public string userPassword { get; set; }
    }
}
