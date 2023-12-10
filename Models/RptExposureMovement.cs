using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class RptExposureMovement
    {
        //  public int ClientID { get; set; }
        //   public string? ClientName { get; set; }
        [Key]
        public int SeriesID { get; set; }
        public DateTime DTE {  get; set; }
        public string? AllungaReference { get; set; }
        public string? ClientReference { get; set; }

        public string? ExpType { get; set; }
        public decimal equivs { get; set; }
        public string? ExpRet { get; set; }
        public int RunningTotalForSeries { get; set; }


//        public string? Name { get; set; }

      // public int ExpTypeID { get; set; }
    }


}
