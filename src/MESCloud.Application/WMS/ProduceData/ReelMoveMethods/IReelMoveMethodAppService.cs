using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.ProduceData.ReelMoveMethods.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReelMoveMethods
{
    public interface IReelMoveMethodAppService : IAsyncCrudAppService<ReelMoveMethodDto, string, PagedResultRequestMESDto, ReelMoveMethodDto, ReelMoveMethodDto>
    {
    }
}
