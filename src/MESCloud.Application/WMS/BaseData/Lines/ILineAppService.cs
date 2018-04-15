using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.BaseData.Storages.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.Lines
{
    public interface ILineAppService : IAsyncCrudAppService<LineDto, string, PagedResultRequestMESDto, LineDto, LineDto>
    {
        Task<ICollection<StorageDto>> GetCStorageByKeyName(string keyName);
        Task<ICollection<StorageDto>> GetSStorageByKeyName(string keyName);
    }
}
