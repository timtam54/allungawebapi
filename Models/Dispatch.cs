using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Dispatch
    {
        public int DispatchID { get; set; }
        public int SeriesID { get; set; }
        public DateTime Dte { get; set; }
        public string? Description { get; set; }
        public int? StaffID { get; set; }
        public bool? ByRequest { get; set; }
        public bool? FullReturn_ElsePart { get; set; }
        public DateTime? ReexposureDate { get; set; }
        public string? Comments { get; set; }

        public string? Status { get; set; }
        public int? SplitFromDispatchID { get; set; }


    }

    public class DispatchStatus
    {
        [Key]
        public string? StatusCode { get; set; }
        public string? StatusDesc { get; set; }
    }

    public class DispatchStaff
    {
        public int DispatchID { get; set; }
        public int SeriesID { get; set; }
        public DateTime Dte { get; set; }
        public string? Description { get; set; }
        public string? Staff { get; set; }
        public bool? ByRequest { get; set; }
        public bool? FullReturn_ElsePart { get; set; }
        public DateTime? ReexposureDate { get; set; }
        public string? Comments { get; set; }

        public string? Status { get; set; }
        public int? SplitFromDispatchID { get; set; }


    }

    

}
