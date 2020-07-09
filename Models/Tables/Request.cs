using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiparisTakip.Models.Tables
{
    public class Request
    {
        [Key]
        public int requestId { get; set; }

        public string requestNo { get; set; }
        public string requestDepartment { get; set; }
        public string requestSteff { get; set; }
        public string requestProject { get; set; }
        public string requestExpensePlace { get; set; }
        public string requestProductFeatures { get; set; }
        public string requestDescription { get; set; }
        public int requestQuantity { get; set; }
        public string requestSpecies { get; set; }
        public string requestImage { get; set; }
        public string requestExcel { get; set; }
        public string requestPDF { get; set; }
        public Double requestEstimatedPrice { get; set; }
        public string requestDeleteDescription { get; set; }

        [Column(TypeName = "Date")]
        public DateTime requestDeliveryDate { get; set; }

        public string requestSupplyCompany1 { get; set; }
        public string requestSupplyCompany2 { get; set; }
        public string requestSupplyCompany3 { get; set; }
        public int requestStatus { get; set; } // 0 - Onay Bekliyor 1 - Tamamlandı
        public string requestCreateAt { get; set; }

        public int userId { get; set; }
        public virtual User user { get; set; }

        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile PDFFile { get; set; }

        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile ExcelFile { get; set; }

        [NotMapped]
        public string date { get; set; }
    }
}