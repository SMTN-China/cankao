using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.ProduceData.ReadyMBills.Dto;
using MESCloud.WMS.ProduceData.ReelSupplyTemps.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReelSupplyTemps
{
    public interface IReelSupplyTempAppService : IAsyncCrudAppService<ReelSupplyTempDto, string, PagedResultRequestMESDto, ReelSupplyTempDto, ReelSupplyTempDto>
    {

        Task<ReelSupplyResultDto> Supply(ReelSupplyInputDto[] input);

        Task<ICollection<ReadyMBillDto>> GetReadyMbillsByKeyName(string keyName);

        Task<ICollection<string>> GetPartNoIdsByKeyName(string readyBill,string keyName);
    }
}
