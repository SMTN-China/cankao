using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.BOMs.Dto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.BaseData.BOMs
{
    public interface IBOMAppService : IAsyncCrudAppService<ProductDto, string, PagedResultRequestMESDto, CreateBOMDto, BOMDto>
    {
        PagedResultDto<BOMDto> GetItemsById(string Id, PagedResultRequestMESDto input);

        Task<ICollection<MPNDto>> GetProductByKeyName(string keyName);

        Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName);

      
    }
}
