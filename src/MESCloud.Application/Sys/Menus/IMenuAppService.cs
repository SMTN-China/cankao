using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.Menus.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MESCloud.Menus
{
    public interface IMenuAppService : IAsyncCrudAppService<MenuDto, int, PagedResultRequestMESDto, CreateMenuDto, MenuDto>
    {
        Task<List<MenuDto>> ChildMenu(int id = -1);


        Task<List<NzTreeDto>> GetNzTreeMenu();

      

       
    }
}
