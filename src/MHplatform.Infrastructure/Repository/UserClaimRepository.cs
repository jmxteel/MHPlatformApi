using MHplatform.Infrastructure.Data;
using MHPlatform.Domain.Entities.Auth;
using MHPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHplatform.Infrastructure.Repository
{
    public class UserClaimRepository: Repository<UserClaim>, IUserClaimRepository
    {
        private readonly DataContext _context;

        public UserClaimRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
