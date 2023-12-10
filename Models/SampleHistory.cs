using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class SampleHistory
    {
        public int SampleID { get; set; }
        public int SampleHistoryID { get; set; } 
        public DateTime? DTE { get; set; }

        public bool? Action { get; set; }
    }


   


}
