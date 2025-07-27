using AutoMapper;
using MHPlatform.Application.DTO;
using MHPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderForm, OrderFormDto>().ReverseMap();
        }
    }
}
