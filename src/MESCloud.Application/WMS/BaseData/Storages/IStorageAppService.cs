using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.Storages.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.Storages
{
    public interface IStorageAppService : IAsyncCrudAppService<StorageDto, string, PagedResultRequestMESDto, StorageDto, StorageDto>
    {
    }
}
