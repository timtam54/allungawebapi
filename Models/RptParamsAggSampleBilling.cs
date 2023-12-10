using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
   // [Keyless]
    public class RptParamsAggSampleBilling
    {
        public int ClientID { get; set; }

        public decimal TotalSamples { get; set; }
        public string? ClientName { get; set; }

        public int ParamID { get; set; }

        public string? ParamName { get; set; }

        public int ReportID { get; set; }

        public string? ReportName { get; set; }

        public string? AllungaReference { get; set; }


        [Key]
        public int SeriesID { get; set; }


//        public string? Name { get; set; }

        //public int ExpTypeID { get; set; }
    }


}
