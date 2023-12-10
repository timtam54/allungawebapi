using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Series
    {
        public int seriesid { get; set; }
        [Display(Name = "SeriesNo")]
        public string? AllungaReference { get; set; }
        public string? clientreference { get; set; }
        public string? ShortDescription { get; set; }
        public DateTime DateIn { get; set; }
        public int clientid { get; set; }
        public string? ExposureSpecification { get; set; }
        public int ExposureTypeID { get; set; }
        public decimal ExposureDurationVal { get; set; }
        public string? ExposureDurationUnit { get; set; }
        public bool Active { get; set; }
        public bool? Deleted { get; set; }
        public string? RackNo { get; set; }
        public decimal ReturnsFrequencyVal { get; set; }
        public string? ReturnsFrequencyUnit { get; set; }
        public DateTime? ExposureEnd { get; set; }

        public bool? SamplesReturned { get; set; }
        public DateTime? NextProjectedReportDate { get; set; }
        public string? NextProjectedReportCalc { get;  set; }
        public string? NextProjectedReportName { get; set; }
        public DateTime? LogBookLetterDate { get; set; }
        public string? LogBookCorrespType { get; set; }
        public int? Site { get; set; }
        public bool? VisualReporting { get; set; }
        public bool? Photos { get; set; }
        public bool returnsreq { get; set; }

        public string? Lock_ComputerName { get; set; }
        public DateTime? Lock_DateTime { get; set; }
    }

    public class SeriesSearch
    {
        [Key]
        public int seriesid { get; set; }
        // [Display(Name = "SeriesNo")]
        public string? AllungaReference { get; set; }
        public string? clientreference { get; set; }

        public bool Complete { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public DateTime DateIn { get; set; }
        public string? companyname { get; set; }

        public string? contactname { get; set; }

        public string? ExposureType { get; set; }
        public decimal ExposureDurationVal { get; set; }
        public string? ExposureDurationUnit { get; set; }

        public int? CntSamplesOnSite { get; set; }
        public string? Abbreviation { get; set; }

        public DateTime? DateNextReturn {  get; set; }
        public DateTime? DateNextReport { get; set; }

        public bool Active { get; set; }

        public bool ReturnsReq { get; set; }

        public string? Locked {  get; set; }

        public string? RackNo { get; set; }

        public string? SiteName { get; set; }
        public decimal? EquivalentSamples { get; set; }
    }


}
