using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Entities.Auth
{
    [Table("UserClaim", Schema = "Security")]
    public class UserClaim
    {
        [Required()]
        [Key()]
        public Guid ClaimId { get; set; }

        [Required()]
        public Guid UserId { get; set; }

        [Required()]
        public string ClaimType { get; set; } = string.Empty;

        [Required()]
        public string ClaimValue { get; set; } = string.Empty;
    }
}
