﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beneficiaries.Core.Models
{
    [Table("BENEFICIARIES")]
    public class BeneficiaryDTO : BasePersonDTO
    {
        [Column("PARTICIPATIONPERCENTAJE"), Required]
        public float ParticipationPercentaje { get; set; }

        [ForeignKey("EMPLOYEES")]
        public double EmployeeId { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
    }
}
