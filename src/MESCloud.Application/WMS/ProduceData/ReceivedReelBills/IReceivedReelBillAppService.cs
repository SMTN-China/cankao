using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.ProduceData.ReceivedReelBills.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReceivedReelBills
{
    public interface IReceivedReelBillAppService : IAsyncCrudAppService<ReceivedReelBillDto, string, PagedResultRequestMESDto, ReceivedReelBillDto, ReceivedReelBillDto>
    {
        Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName);
    }
}
