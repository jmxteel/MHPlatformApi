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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
