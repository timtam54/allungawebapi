using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Master
    {
        public int id { get; set; }
        public string? site { get; set; }
        public DateTime? date { get; set; }
        public string? uniquesampleid { get; set; }
        public string? analysisrequired { get; set; }
        public DateTime SampleDate { get; set; }
        public string? SampleType { get; set; }
        public string? Shift { get; set; }
        public int? pumpno { get; set; }

        public string? samplerno { get; set; }
        public string? filterno { get; set; }
        public string? SamplerType { get; set; }
        public string? STComments { get; set; }
        public int? flowon { get; set; }
        public int? flowoff { get; set; }

        public decimal? respduststd { get; set; }
        public decimal? shiftadjrespdust { get; set; }
        public decimal? respdustconc { get; set; }
        public decimal? respcrystsilicastd { get; set; }

    }
}
