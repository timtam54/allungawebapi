using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class ExposureType
    {
        public int ExposureTypeID { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int? ExposureSampleRateGroupsID { get; set; }
        public decimal? OnSiteRateStandard { get; set; }
        public decimal? OnSiteRateDiscount { get; set; }
        public int? SortOrder { get; set; }

       
    }





}
