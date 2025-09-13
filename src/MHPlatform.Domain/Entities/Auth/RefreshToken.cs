using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Entities.Auth
{
    [Table("RefreshToken", Schema = "Security")]
    public class RefreshToken
    {
        [Key()]
        public int ID { get; set; }

        [Required()]
        public string UserId { get; set; } = string.Empty;

        public string? Token { get; set; } = string.Empty;
    }
}
