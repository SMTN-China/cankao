using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.ProduceData.ReadyMBillDetaileds.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReadyMBillDetaileds
{
    public interface IReadyMBillDetailedAppService : IApplicationService
    {
        Task<PagedResultDto<ReadyMBillDetailedReportDto>> GetAllAsync(string readyBillId, PagedResultRequestMESDto input);

       
    }
}
