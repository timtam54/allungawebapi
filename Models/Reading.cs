using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Reading
    {
        public int Readingid { get; set; }
        public int Paramid { get; set; }

        public int sampleid { get; set; }

        [Display(Name = "Value")]
        public string? value { get; set; }
       

        public int reportid { get; set; }

        public bool? deleted { get; set; }

    }


   


}
