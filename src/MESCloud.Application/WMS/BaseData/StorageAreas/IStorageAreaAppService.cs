using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.BaseData.StorageAreas.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.StorageAreas
{
    public interface IStorageAreaAppService : IAsyncCrudAppService<StorageAreaDto, string, PagedResultRequestMESDto, StorageAreaDto, StorageAreaDto>
    {
        Task<ICollection<string>> GetShelfByKeyName(string keyName);

        Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName);

    }
}
