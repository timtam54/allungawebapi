using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class RptSampleOnSite
    {
        public int ClientID { get; set; }
        public string? ClientName { get; set; }
        
        public int SeriesID { get; set; }

        public string? SeriesName { get; set; }

        public int MonthNo { get; set; }
        public int Year { get; set; }

        public string? ExpTypeNameOrGroup { get; set; }

        public int EquivSampleCount { get; set; }

        public int? ID { get; set; }

//        public string? Name { get; set; }

        public int ExpTypeID { get; set; }
    }


}
