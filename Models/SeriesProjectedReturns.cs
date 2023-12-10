using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class SeriesProjectedReturns
    {
        public int id { get; set; }
        public int seriesid { get; set; }
    
        public DateTime ReturnDate { get; set; }
    
        public string? Samples { get; set; }

        public string? SeriesProjectedReturnCalc { get; set; }
        public string? ReturnName { get; set; }
        public int? cnt { get; set; }

    }



}
