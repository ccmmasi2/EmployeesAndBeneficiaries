using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beneficiaries.Core.Models
{
    [Table("BENEFICIARIES")]
    public class BeneficiarieDTO : BasePersonDTO
    {
        [Column("PARTICIPATIONPERCENTAJE"), Required]
        public float ParticipationPercentaje { get; set; }
    }
}
