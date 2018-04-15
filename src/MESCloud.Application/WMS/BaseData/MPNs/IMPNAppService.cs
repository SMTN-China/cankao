using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.Customers.Dto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.MPNs
{
    public interface IMPNAppService : IAsyncCrudAppService<MPNDto, string, PagedResultRequestMESDto, MPNDto, MPNDto>
    {
        Task<ICollection<CustomerDto>> GetCustomerByKeyName(string keyName);

        Task<ICollection<CustomerDto>> GetCustomerById(string id);

        Task<bool> BatchInsOrUpdate(ICollection<MPNDto> input);
    }
}
