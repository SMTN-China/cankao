using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.BaseData.Storages.Dto;
using MESCloud.WMS.ProduceData.Reels.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.Reels
{
    public interface IReelAppService : IAsyncCrudAppService<ReelDto, string, PagedResultRequestMESDto, ReelDto, ReelDto>
    {
        Task<ReelMoveResDto> ReelMove(ReelMoveDto inputDto);

        Task<NextFistReelDto> GetNextFistReel(string readyMId);


        Task<PagedResultDto<ReelOutLifeDto>> GetOutLifeReel(PagedResultRequestMESDto input);

        Task UpdateReelESL(UpdateReelESLDto updateReelESL);

        Task<PagedResultDto<GroupReelDto>> GetGroupReel(PagedResultRequestMESDto input);

        Task<ICollection<StorageDto>> GetStorageByKeyName(string keyName);

        Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName);

        Task BrightByPartNoIds(LightOrderDto[] pns);

        Task BrightByReelIds(LightOrderDto[] reels);

        Task ExtinguishedByPartNoIds(LightOrderDto[] pns);

        Task ExtinguishedByReelIds(LightOrderDto[] reels);

        Task<GetReceivedsResult> GetReceiveds(ReelMoveDto inputDto);

        Task ReturnReel(ReturnReelDto returnReel);
    }
}
