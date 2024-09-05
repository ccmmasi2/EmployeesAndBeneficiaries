using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beneficiaries.Core.Models
{
    [Table("EMPLOYEES")]
    public class EmployeeDTO : BasePersonDTO
    {
        [Column("EMPLOYEENUMBER"), Required]
        public double EmployeeNumber { get; set; }
    }
}
