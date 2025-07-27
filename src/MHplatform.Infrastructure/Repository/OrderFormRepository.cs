using MHplatform.Infrastructure.Data;
using MHPlatform.Application.Interface;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHplatform.Infrastructure.Repository
{
    public class OrderFormRepository : Repository<OrderForm>, IOrderFormRepository
    {
        private readonly DataContext _context;

        public OrderFormRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
