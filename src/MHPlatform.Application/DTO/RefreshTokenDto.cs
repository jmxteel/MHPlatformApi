using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.DTO
{
    public class RefreshTokenDto
    {
        public int ID { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string? Token { get; set; } = string.Empty;
    }
}
