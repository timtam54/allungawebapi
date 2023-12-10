using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Param
    {
        public int ParamID { get; set; }
        [Display(Name = "Parameter Name")]
        public string? ParamName { get; set; }
        public string? Unit { get; set; }

        public string? ValueRange { get; set; }

        public string? EquivalentValues { get; set; }

        public int Ordering { get; set; }

        public string? VisualNoReadings { get; set; }
        public int? ReportParamCostGroupID { get; set; }

        public decimal? ReportRateDiscounted { get; set; }

        public decimal? ReportRateStandard { get; set; }
    }


   


}
