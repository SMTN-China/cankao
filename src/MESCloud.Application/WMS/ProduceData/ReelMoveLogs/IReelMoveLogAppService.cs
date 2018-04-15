using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.ProduceData.ReelMoveLogs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReelMoveLogs
{
    public interface IReelMoveLogAppService: IApplicationService
    {
        Task<PagedResultDto<ReelMoveLogDto>> GetAllAsync(string reelMoveMethodId, PagedResultRequestMESDto input);
    }
}
