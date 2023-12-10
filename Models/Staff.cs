using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Staff
    {
        public int StaffID { get; set; }

        public string? StaffName { get; set; }

        public bool? defaultSig { get; set; }


    }
}
