using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class ReportParam
    {
       public int id { get; set; }
        public int reportid { get; set; }

        public int paramid { get; set; }
        public bool? deleted { get; set; }

    }

    public class ReportParamDesc
    {
        public int id { get; set; }
        public int reportid { get; set; }
        public int paramid { get; set; }

        public string? paramname { get; set; }
        public bool? selected { get; set; }

    }



}
