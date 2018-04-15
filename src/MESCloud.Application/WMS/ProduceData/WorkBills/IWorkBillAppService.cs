using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.BOMs.Dto;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.ProduceData.WorkBills.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.WorkBills
{
    public interface IWorkBillAppService : IAsyncCrudAppService<WorkBillDto, string, PagedResultRequestMESDto, WorkBillDto, WorkBillDto>
    {
        Task<ICollection<LineDto>> GetLineByKeyName(string keyName);

        Task<ICollection<ProductDto>> GetProductByKeyName(string keyName);
    }
}
