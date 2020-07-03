using SiparisTakip.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string userPermission { get; set; }
        public List<Request> requests { get; set; }
    }
}