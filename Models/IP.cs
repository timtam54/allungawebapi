using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class IP
    {
        [Key]
        public int ID { get; set; }
        
        public string? IPAddress { get; set; }
        
    }


   


}
