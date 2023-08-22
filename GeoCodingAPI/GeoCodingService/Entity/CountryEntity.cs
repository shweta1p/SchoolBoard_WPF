using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingService.Entity
{
    [Table("COUNTRIES", Schema = "dbo")]
    public class CountryEntity
    {
        [Key]
        public int ID { get; set; }
        public string Country { get; set; }
        public byte[] Image { get; set; }
    }
}
