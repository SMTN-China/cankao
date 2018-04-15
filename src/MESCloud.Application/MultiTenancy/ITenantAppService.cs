using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.MultiTenancy.Dto;

namespace MESCloud.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
