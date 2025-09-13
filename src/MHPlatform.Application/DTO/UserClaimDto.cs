using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.DTO
{
    public class UserClaimDto
    {
        public Guid ClaimId { get; set; }

        public Guid UserId { get; set; }

        public string ClaimType { get; set; } = string.Empty;

        public string ClaimValue { get; set; } = string.Empty;
    }
}
