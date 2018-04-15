using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.ProduceData.UPHs.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.UPHs
{
    [AbpAuthorize(PermissionNames.Pages_UPHs)]
    public class UPHAppService : AsyncCrudAppService<UPH, UPHDto, string, PagedResultRequestMESDto, UPHDto, UPHDto>, IUPHAppService
    {
        readonly IRepository<UPH, string> _repository;
        readonly IRepository<MPN, string> _repositoryMPN;
        readonly IRepository<Line, string> _repositoryLine;

        public UPHAppService(IRepository<UPH, string> repository, IRepository<MPN, string> repositoryMPN, IRepository<Line, string> repositoryLine) : base(repository)
        {
            _repository = repository;
            _repositoryMPN = repositoryMPN;
            _repositoryLine = repositoryLine;
        }

        [HttpPost]
        public async override Task<PagedResultDto<UPHDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<UPH>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<UPHDto>(tasksCount, taskList.MapTo<List<UPHDto>>());
        }

        public async Task<ICollection<MPNDto>> GetProductByKeyName(string keyName)
        {
            var res = await _repositoryMPN.GetAll().Where(c =>c.Id.Contains(keyName)).Take(10).ToListAsync(); ;
            return Mapper.Map<List<MPN>, List<MPNDto>>(res);
        }
        public async Task<ICollection<LineDto>> GetLineByKeyName(string keyName)
        {
            var res = await _repositoryLine.GetAll().Where(l => l.Id.Contains(keyName)).Take(10).ToListAsync(); ;

            return Mapper.Map<List<Line>, List<LineDto>>(res);
        }
    }
}
