using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Audit
    {
        public int AuditID { get; set; }
        [Display(Name = "Table Name")]
        public string? TableName { get; set; }
        public int PKey { get; set; }

        public string? Descript { get; set; }

        public DateTime? ModDate { get; set; }

        public string? WinUserName { get; set; }
      
    }

}
