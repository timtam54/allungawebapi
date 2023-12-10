using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class EvTyp
    {
        [Key]
        public string ID { get; set; }
        
        public string? EventDesc { get; set; }
        
    }


   


}
