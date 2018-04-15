using AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.Customers.Dto
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();

        }
    }
}
