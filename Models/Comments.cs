using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Comments
    {
        [Key]
        public int CommentID { get; set; }
        public int ReportID { get; set; }

        public int SampleID { get; set; }

        public string? Comment { get; set; }

        public int? SplitFromReportID { get; set; }


    }


   


}
