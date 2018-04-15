using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.Customers
{
    public interface ICustomerAppService : IAsyncCrudAppService<CustomerDto, string, PagedResultRequestMESDto, CustomerDto, CustomerDto>
    {
    }
}
