using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beneficiaries.Core.Models
{
    public class BasePersonDTO
    {
        [Key]
        public int ID { get; set; }

        [Column("NAME"), MaxLength(50), Required]
        public string Name { get; set; }

        [Column("LASTNAME"), MaxLength(50), Required]
        public string LastName { get; set; }

        [Column("BIRTHDAY"), Required]
        public DateTime BirthDay { get; set; }

        [Column("CURP"), Required]
        public string CURP { get; set; }

        [Column("SSN"), Required]
        public string SSN { get; set; }

        [Column("PHONENUMBER"), Required]
        public string PhoneNUmber { get; set; }

        [ForeignKey("COUNTRIES")]
        public int CountryId { get; set; }

        public virtual CountryDTO Country { get; set; }
    }
}
