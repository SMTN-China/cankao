using System.Threading.Tasks;
using Abp.Application.Services;
using MESCloud.Sessions.Dto;

namespace MESCloud.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
