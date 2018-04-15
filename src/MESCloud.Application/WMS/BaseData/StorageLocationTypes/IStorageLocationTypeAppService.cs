using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.StorageLocationTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageLocationTypes
{
   public interface IStorageLocationTypeAppService : IAsyncCrudAppService<StorageLocationTypeDto, string, PagedResultRequestMESDto, StorageLocationTypeDto, StorageLocationTypeDto>
    {
    }
}
