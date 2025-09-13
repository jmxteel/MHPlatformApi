using MHPlatform.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.Interface
{
    public interface ISecurityService
    {
        Task<UserAuthBaseDto> LoginAsync(string username, string password);
        bool ValidateExpiry(string? refreshToken);
    }
}
