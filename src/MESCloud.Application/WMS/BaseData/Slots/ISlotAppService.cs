using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.BOMs.Dto;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.BaseData.Slots.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.Slots
{
 public   interface ISlotAppService : IAsyncCrudAppService<SlotDto, int, PagedResultRequestMESDto, SlotDto, SlotDto>
    {
        Task<ICollection<LineDto>> GetLineByKeyName(string keyName);

        Task<ICollection<MPNDto>> GetProductByKeyName(string keyName);

        Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName);

        Task<ICollection<BatchSlotListDto>> BatchEdit(BatchSlotDto batchSlot);
    }
}
