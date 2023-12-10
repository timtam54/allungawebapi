using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class Client
    {
        public int clientid { get; set; }
        [Display(Name = "Name")]
        public string? companyname { get; set; }
        public string? groupName { get; set; }
        public string contactname { get; set; }
          public string? address { get; set; }
          public string? description { get; set; }

        public string? abbreviation { get; set; }


        public string? TechnicalRole { get; set; }
               public string? TechnicalTitle { get; set; }

        public string? TechnicalMobile { get; set; }

          public string? TechnicalPhone { get; set; }
          public string? TechnicalFax { get; set; }

        public string? TechnicalEmail { get; set; }

        public string? AccountingContact { get; set; }
        public string? AccoutingPhone { get; set; }
        public string? AccountingFax { get; set; }
        public string? AccountingEmail { get; set; }
        public string? AccountingMobile { get; set; }
        public string? AccountAddress {  get; set; }

        public string? AccountRole { get; set; }

        public string? AccountingDesc { get; set; }



        public string? AccountingTitle { get; set; }
        public string? Website { get; set; }

        public string? FreightContact { get; set; }
        public string? FreightPhone { get; set; }

        public string? FreightMobile { get; set; }

        public string? FreightEmail { get; set; }

        public string? FreightFax { get; set; }

        public string? FreightAddress { get; set; }
        public string? FreightDesc { get; set; }
    }



}
