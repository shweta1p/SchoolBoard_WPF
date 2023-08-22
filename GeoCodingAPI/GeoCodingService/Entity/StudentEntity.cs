using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoCodingService.Entity
{
    [Table("STUDENT", Schema = "dbo")]
    public class StudentEntity
    {
        [Key]
        public int ID { get; set; }
        public string LAST_NAME { get; set; }
        public string FIRST_NAME { get; set; }
        public DateTime BIRTHDATE { get; set; }
        public string DIVERSITY { get; set; }
        public string ORIGIN_OF_BIRTH { get; set; }
        public string CITIZENSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string GENDER { get; set; }
        public string ETHNICITY { get; set; }
        public string HOMEROOM { get; set; }
        public string EMAIL { get; set; }
        public string STUDENT_ID { get; set; }
        public int SCHOOL_ID { get; set; }
        public bool IsDeleted { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string Distance { get; set; }
        public string Distance1 { get; set; }
    }
}
