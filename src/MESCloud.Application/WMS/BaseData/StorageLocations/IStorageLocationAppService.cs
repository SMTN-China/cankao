using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.StorageLocations.Dto;
using MESCloud.WMS.BaseData.StorageLocationTypes.Dto;
using MESCloud.WMS.BaseData.Storages.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.StorageLocations
{
    public interface IStorageLocationAppService : IAsyncCrudAppService<StorageLocationDto, string, PagedResultRequestMESDto, StorageLocationDto, StorageLocationDto>
    {

        Task<ICollection<StorageLocationTypeDto>> GetStorageLocationTypeByKeyName(string keyName);

        Task<ICollection<StorageDto>> GetStorageByKeyName(string keyName);

        Task<bool> GetIsHave(string id);

        Task AddByLY(LYDto lYDto);

        Task AddByHF(HFDto hFDto);

        Task AllBright();

        Task NonReelBright();

        Task AllExtinguished();
    }
}
