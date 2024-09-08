using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beneficiaries.Core.Models
{
    [Table("BENEFICIARIES")]
    public class BeneficiaryDTO : BasePersonDTO
    {
        [Column("PARTICIPATIONPERCENTAJE"), Required]
        public int ParticipationPercentaje { get; set; }

        [ForeignKey("EMPLOYEES")]
        public Int64 EmployeeId { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
    }
}
