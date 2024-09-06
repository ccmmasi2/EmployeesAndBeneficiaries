using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beneficiaries.Core.Models
{
    public class BasePersonDTO
    {
        [Key]
        public Int64 ID { get; set; }

        [Column("NAME"), MaxLength(50), Required]
        public string Name { get; set; }

        [Column("LASTNAME"), MaxLength(50), Required]
        public string LastName { get; set; }

        [Column("BIRTHDAY"), Required]
        public DateTime BirthDay { get; set; }

        [Column("CURP"), MaxLength(10), Required]
        public string CURP { get; set; }

        [Column("SSN"), MaxLength(10), Required]
        public string SSN { get; set; }

        [Column("PHONENUMBER"), MaxLength(10), Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("COUNTRIES")]
        public int CountryId { get; set; }

        public virtual CountryDTO Country { get; set; }
    }
}
