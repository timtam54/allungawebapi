using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Sample
    {
        public int SampleID { get; set; }
        [Display(Name = "Sample No")]
        public int Number { get; set; }
        public string? description { get; set; }
        public string? longdescription { get; set; }
        public int ExposureTypeID { get; set; }

        public bool? Reportable { get; set; }

        public bool? Deleted { get; set; }
        public int seriesid { get; set; }
        public decimal? EquivalentSamples { get; set; }
        public int? SampleOrder { get; set; }
        public bool? OnSite_Temp { get; set; }
    }


    public class SampleSearch
    {
        public int SampleID { get; set; }
        [Display(Name = "Sample No")]
        public int Number { get; set; }
        public string? description { get; set; }
        public string? longdescription { get; set; }
        public string? ExposureType { get; set; }

        public decimal? EquivalentSamples { get; set;}

        public bool? Reportable { get; set; }
        public int seriesid { get; set; }
        public int? SampleOrder { get; set; }
    }


}
