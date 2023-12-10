using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
   // [Keyless]
    public class RptExposureOnSiteMonthBilling
    {
        public int ClientID { get; set; }
        public string? ClientName { get; set; }
        [Key]
        public int SeriesID { get; set; }

        public string? SeriesName { get; set; }

        public int MonthNo { get; set; }
        public int Year { get; set; }

        public string? ExpTypeNameOrGroup { get; set; }

        public int EquivSampleCount { get; set; }

        public int ExposureTypeID { get; set; }


//        public string? Name { get; set; }

        //public int ExpTypeID { get; set; }
    }


}
