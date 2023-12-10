using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Report
    {
        public int reportid { get; set; }
        [Display(Name = "Report Name")]
        public string? reportname { get; set; }
        public DateTime date { get; set; }

        public string? bookandpage { get; set; }

        public string? reportstatus { get; set; }

        public int seriesid { get; set; }

        public bool? return_elsereport { get; set; }
        public bool? deleted { get; set; }

        public string? comment { get; set; }

        public DateTime? completeddate { get; set; }

    }
}
