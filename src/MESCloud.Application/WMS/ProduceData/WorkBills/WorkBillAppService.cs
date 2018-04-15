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
using MESCloud.WMS.BaseData.BOMs.Dto;
using MESCloud.WMS.BaseData.Lines.Dto;
using MESCloud.WMS.ProduceData.WorkBills.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.WorkBills
{
    [AbpAuthorize(PermissionNames.Pages_WorkBills)]
    public class WorkBillAppService : AsyncCrudAppService<WorkBill, WorkBillDto, string, PagedResultRequestMESDto, WorkBillDto, WorkBillDto>, IWorkBillAppService
    {
        readonly IRepository<WorkBill, string> _repositoryWorkBill;
        readonly IRepository<Line, string> _repositoryLine;
        readonly IRepository<MPN, string> _repositoryMPN;
        public WorkBillAppService(IRepository<WorkBill, string> repositoryWorkBill, IRepository<Line, string> repositoryLine, IRepository<MPN, string> repositoryMPN) : base(repositoryWorkBill)
        {
            _repositoryWorkBill = repositoryWorkBill;
            _repositoryLine = repositoryLine;
            _repositoryMPN = repositoryMPN;
        }
        [HttpPost]
        public async override Task<PagedResultDto<WorkBillDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<WorkBill>(input, _repositoryWorkBill.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<WorkBillDto>(tasksCount, taskList.MapTo<List<WorkBillDto>>());
        }

        public async Task<ICollection<LineDto>> GetLineByKeyName(string keyName)
        {
            var res = await _repositoryLine.GetAll().Where(l => l.Id.Contains(keyName)).Take(10).ToListAsync(); ;

            return Mapper.Map<List<Line>, List<LineDto>>(res);
        }

        public async Task<ICollection<ProductDto>> GetProductByKeyName(string keyName)
        {
            var res = await _repositoryMPN.GetAll().Where(l => l.MPNHierarchy == MPNHierarchy.Product && l.Id.Contains(keyName)).Take(10).ToListAsync(); ;

            return Mapper.Map<List<MPN>, List<ProductDto>>(res);
        }
    }
}
