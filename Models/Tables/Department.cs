using System.ComponentModel.DataAnnotations;

namespace SiparisTakip.Models.Tables
{
    public class Department
    {
        [Key]
        public int depId { get; set; }

        public string depName { get; set; }
    }
}