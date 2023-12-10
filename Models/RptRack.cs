using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    [Keyless]
    public class RptRack
    {
        public int ClientID { get; set; }

        public string ClientName { get; set; }
        public int SeriesID { get; set; }

        public int Samples { get; set; }

        public string? AllungaReference { get; set; }

        public string? ClientReference { get; set; }

        public string? RackNo { get; set; }
    }


}
