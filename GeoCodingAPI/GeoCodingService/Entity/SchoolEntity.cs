using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoCodingService.Entity
{
    [Table("SCHOOL", Schema = "dbo")]
    public class SchoolEntity
    {
        [Key]
        public int ID { get; set; }
        public string SCHOOL_NAME { get; set; }
        public int SCHOOLBOARD_ID { get; set; }
        public string ADDRESS { get; set; }
        public string DISTRICT { get; set; }
        public string SCHOOL_TYPE { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }

    }
}
