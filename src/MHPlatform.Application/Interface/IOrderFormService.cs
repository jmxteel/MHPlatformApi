using MHPlatform.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.Interface
{
    public interface IOrderFormService
    {
        Task<OrderFormDto?> GetCustomerById(int id);
    }
}
