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
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.ProduceData.ReceivedReelBills.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.ReceivedReelBills
{
    [AbpAuthorize(PermissionNames.Pages_ReceivedReelBills)]
    public class ReceivedReelBillAppService : AsyncCrudAppService<ReceivedReelBill, ReceivedReelBillDto, string, PagedResultRequestMESDto, ReceivedReelBillDto, ReceivedReelBillDto>, IReceivedReelBillAppService
    {
        readonly IRepository<ReceivedReelBill, string> _repository;
        readonly IRepository<MPN, string> _repositoryMPN;

        public ReceivedReelBillAppService(IRepository<ReceivedReelBill, string> repository, IRepository<MPN, string> repositoryMPN) : base(repository)
        {
            _repository = repository;
            _repositoryMPN = repositoryMPN;
        }

        [HttpPost]
        public async override Task<PagedResultDto<ReceivedReelBillDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<ReceivedReelBillDto>(tasksCount, taskList.MapTo<List<ReceivedReelBillDto>>());
        }


        public async Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName)
        {
            var res = await _repositoryMPN.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync(); ;
            return Mapper.Map<List<MPN>, List<MPNDto>>(res);
        }
    }
}
