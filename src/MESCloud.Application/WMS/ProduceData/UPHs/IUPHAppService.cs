using Abp.Application.Services;
using MESCloud.CommonDto;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.ProduceData.UPHs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.UPHs
{
  public  interface IUPHAppService  : IAsyncCrudAppService<UPHDto, string, PagedResultRequestMESDto, UPHDto, UPHDto>
    {
        Task<ICollection<LineDto>> GetLineByKeyName(string keyName);

        Task<ICollection<MPNDto>> GetProductByKeyName(string keyName);
    }
}
