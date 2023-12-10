using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class DispatchSample
    {
        [Key]
        public int DispatchSampleID { get; set; }
        public int DispatchID { get; set; }
        public int SampleID { get; set; }
        public decimal? EquivalentSamples { get; set; }
        public string? Description { get; set; }
        public int? Number { get; set; }
        public string? LongDescription { get; set; }
        public int? SplitFromDispatchSampleID { get; set; }
    }

    public class DispatchSampleRpt
    {
        [Key]
        public int DispatchSampleID { get; set; }
        public int DispatchID { get; set; }
        public int SampleID { get; set; }
        public string? SampeClientRef { get; set; }

        public int? SampleAlRef { get; set; }
        public decimal? EquivalentSamples { get; set; }
        public string? Description { get; set; }
        public int? Number { get; set; }
        public string? LongDescription { get; set; }
        public int? SplitFromDispatchSampleID { get; set; }
    }

}
