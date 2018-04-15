using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.WMS.BaseData.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using Microsoft.AspNetCore.Mvc;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.AutoMapper;

namespace MESCloud.WMS.BaseData.Customers
{
    [AbpAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, string, PagedResultRequestMESDto, CustomerDto, CustomerDto>, ICustomerAppService
    {
        private readonly IRepository<Customer,string> _repository;
        public CustomerAppService(IRepository<Customer, string> repository) : base(repository)
        {
            this._repository = repository;
        }
        [HttpPost]
        public async override Task<PagedResultDto<CustomerDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Customer>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<CustomerDto>(tasksCount, taskList.MapTo<List<CustomerDto>>());
        }
    }
}
