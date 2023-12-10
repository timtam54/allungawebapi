using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AllungaWebAPI.Models
{
    public class SeriesEvent
    {
        [Key]
        public int Id { get; set; }
        public int SeriesID { get; set; }
        public string EventType { get; set; }

        public decimal FrequencyVal { get; set; }

        public string? FrequencyUnit { get; set; }    

        public DateTime? EstNextDate { get; set; }

    }


    public class SeriesEventRpt
    {

        public int Id { get; set; }
        public int SeriesID { get; set; }
        [Key]
        public string EventType { get; set; }

        public string EventDesc { get; set; }

        public decimal? FrequencyVal { get; set; }

        public string? FrequencyUnit { get; set; }

        public DateTime? EstNextDate { get; set; }

    }


}
