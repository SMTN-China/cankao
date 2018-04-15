using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.Entities;
using MESCloud.Sys.I18Ns.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Reflection.Extensions;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MESCloud.Sys.I18Ns
{
    [AbpAuthorize(PermissionNames.Pages_Orgs)]
    public class I18NAppService : AsyncCrudAppService<I18N, I18NDto, int, PagedResultRequestMESDto, CreateI18NDto, I18NDto>, II18NAppService
    {
        readonly IRepository<I18N, int> _repository;
        public I18NAppService(IRepository<I18N, int> repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async override Task<PagedResultDto<I18NDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<I18N>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<I18NDto>(tasksCount, taskList.MapTo<List<I18NDto>>());

           // return base.GetAll(input);
        }

        public override Task<I18NDto> Create(CreateI18NDto input)
        {
            input.TenantId = AbpSession.TenantId;
            return base.Create(input);
        }

        public List<string> GetDtoByKeyName(string keyName)
        {
            var thisAssembly = typeof(MESCloudApplicationModule).GetAssembly().ExportedTypes.Where(t => t.Name.Contains("Dto") && t.Name.ToLower().Contains(keyName.ToLower())).Take(10).ToList();
            if (thisAssembly == null)
            {
                return new List<string>();
            }
            return thisAssembly.Select(t => t.Name).ToList();
        }

        public List<string> GetPropertyByDtoName(string dtoName)
        {
            var thisAssembly = typeof(MESCloudApplicationModule).GetAssembly().ExportedTypes.Where(t => t.Name.ToLower() == dtoName.ToLower()).FirstOrDefault();
            if (thisAssembly == null)
            {
                return new List<string>();
            }
            var propert = thisAssembly.GetProperties().Select(p => p.Name).ToList();

            return propert;
        }
    }
}
