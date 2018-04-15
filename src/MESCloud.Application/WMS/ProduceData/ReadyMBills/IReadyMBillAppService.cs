using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.ProduceData.ReadyMBills.Dto;
using MESCloud.WMS.ProduceData.ReelMoveMethods.Dto;
using MESCloud.WMS.ProduceData.WorkBills.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReadyMBills
{
    public interface IReadyMBillAppService : IAsyncCrudAppService<ReadyMBillDto, string, PagedResultRequestMESDto, ReadyMBillDto, ReadyMBillDto>
    {
        Task<ICollection<WorkBillDto>> GetWorkBillByKeyName(string keyName);

        Task<ICollection<ReelMoveMethodDto>> GetReelMoveMethodByKeyName(string keyName);

        Task<ICollection<ReadyMBillDto>> GetFollowReadyMBillKeyName(string keyName);

        Task<ReadyMResultDto> ReadyM(ReadyMDto readyM);

        Task<ReadyMResultDto> ReadyFirstM(ReadyMDto readyM);

        Task<PagedResultDto<ReelSendTempDto>> GetAllSendKanban(PagedResultRequestMESDto input);

        Task<PagedResultDto<ReelShortTempDto>> GetAllShortKanban(PagedResultRequestMESDto input);

        Task<bool> RBBatchInsOrUpdate(ICollection<RBBatchReadyMBillDto> input);

        Task<ICollection<ReadyMBillDetailedDto>> GetDetailedById(string id);

        Task<ICollection<ReadyMBillWorkBillMapDto>> GetWorkBillById(string id);


    }
}
