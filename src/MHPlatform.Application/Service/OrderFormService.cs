using AutoMapper;
using MHPlatform.Application.DTO;
using MHPlatform.Application.Interface;
using MHPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.Service
{
    public class OrderFormService : IOrderFormService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderFormService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderFormDto?> GetCustomerById(int id)
        {
            var cutomerData = await _unitOfWork.OrderFormRepository.GetByIdAsync(id);

            return cutomerData is null? null : _mapper.Map<OrderFormDto>(cutomerData);
        }
    }
}
