using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoCodingService.Entity
{
    [Table("BarcelonaList", Schema = "dbo")]
    public class PlacesEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
        public string PhoneNumber { get; set; }
        public string Sector { get; set; }        
        public string Address { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
    }
}
