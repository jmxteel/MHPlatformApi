using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.DTO
{
    public class UserAuthBaseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string BearerToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
    }
}
