using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class SeriesNextEvent
    {
        public int seriesid { get; set; }
        [Display(Name = "SeriesNo")]
        public DateTime NextEventDate { get; set; }
        public string? EventType { get; set; }
        public string? NextEventName { get; set; }
        public string? NextEventDesc { get; set; }
        public string? ClientName { get; set; }
        public int ClientID { get; set; }
        public string? AllungaRef { get; set; }
      //  public int ReportID { get; set; }
    }
}
