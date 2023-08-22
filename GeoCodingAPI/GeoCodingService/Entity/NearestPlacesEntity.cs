using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoCodingService.Entity
{
    [Table("NearestPlaces", Schema = "dbo")]
    public class NearestPlacesEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string nLatitude { get; set; }
        public string nLongitude { get; set; }
        public string nName { get; set; }
        public string nRating { get; set; }
        public string nAddress { get; set; }
    }
}
