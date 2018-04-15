using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MESCloud.CommonDto;
using MESCloud.WMS.ProduceData.ReadyMBillDetaileds.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.Domain.Repositories;
using MESCloud.Entities.WMS.ProduceData;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using MESCloud.WMS.ProduceData.ReelMoveLogs.Dto;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using MESCloud.Authorization;

namespace MESCloud.WMS.ProduceData.ReadyMBillDetaileds
{
    [AbpAuthorize(PermissionNames.Pages_ReadyMBillDetaileds)]
    public class ReadyMBillDetailedAppService : ApplicationService, IReadyMBillDetailedAppService
    {
        readonly IRepository<ReadyMBill, string> _repositoryReadyMBill;
        readonly IRepository<ReadyMBillDetailed, string> _repositoryReadyMBilld;
        public ReadyMBillDetailedAppService(IRepository<ReadyMBill, string> repositoryReadyMBill, IRepository<ReadyMBillDetailed, string> repositoryReadyMBilld)
        {

            _repositoryReadyMBill = repositoryReadyMBill;
            _repositoryReadyMBilld = repositoryReadyMBilld;
        }
        [HttpPost]

        public async Task<PagedResultDto<ReadyMBillDetailedReportDto>> GetAllAsync(string readyBillId, PagedResultRequestMESDto input)
        {
            var resS = await

             Task.Factory.StartNew(() =>
             {



                 // 获取输入备料单的记账备料单
                 var readyBill = _repositoryReadyMBill.FirstOrDefault(readyBillId);

                 if (readyBill == null)
                 {
                     return new PagedResultDto<ReadyMBillDetailedReportDto>(0, new List<ReadyMBillDetailedReportDto>());
                 }

                 // 获取筛选条件中的 备料单 合集
                 var readyBills = _repositoryReadyMBill.GetAll().Where(s => s.ReReadyMBillId == readyBill.ReReadyMBillId).Include(r => r.WorkBills).ThenInclude(s => s.WorkBill).ToList();

                 // 查询记账备料单的相信信息
                 var ReadyMBillds = _repositoryReadyMBilld.GetAll().Where(s => readyBills.Select(r => r.Id).Contains(s.ReadyMBillId)).ToList();

                 var toList = new Func<IEnumerable<IEnumerable<string>>, string>(i =>
                 {
                     var list = new List<string>();
                     foreach (var item in i)
                     {
                         foreach (var str in item)
                         {
                             list.Add(str);
                         }
                     }

                     return string.Join('|', list.Distinct());
                 });

                 // 分组
                 var res = ReadyMBillds.GroupBy(r => r.PartNoId).Select(s => new ReadyMBillDetailedReportDto()
                 {
                     ReReadyMBillId = readyBill.ReReadyMBillId,
                     WorkBillIds = toList(readyBills.Select(r => r.WorkBills).Select(w => w.Select(wo => wo.WorkBillId + ":" + wo.Qty)).Distinct()),
                     Products = toList(readyBills.Select(r => r.WorkBills).Select(w => w.Select(wo => wo.WorkBill.ProductId)).Distinct()),
                     Lines = toList(readyBills.Select(r => r.WorkBills).Select(w => w.Select(wo => wo.WorkBill.LineId)).Distinct()),
                     DemandQty = s.Sum(r => r.Qty),
                     SendQty = s.Sum(r => r.SendQty),
                     PartNoId = s.Key,
                     FollowQty = s.Sum(r => r.FollowQty),
                     ReadyMBillIds = string.Join('|', readyBills.Select(r => r.Id)),
                     ReelMoveMethodId = s.FirstOrDefault().ReelMoveMethodId,
                     ReturnQty = s.Sum(r => r.ReturnQty),
                     MoreSendQty = s.Sum(r => r.SendQty) - s.Sum(r => r.Qty) - s.Sum(r => r.ReturnQty),
                     NoFollowQty = s.Sum(r => r.SendQty) - s.Sum(r => r.FollowQty),
                     RealSendQty = s.Sum(r => r.SendQty) - s.Sum(r => r.ReturnQty)
                 });

                 var query = MESPagedResult.GetMESPagedResult<ReadyMBillDetailedReportDto>(input, res.AsQueryable());

                 var tasksCount = query.Count();

                 //默认的分页方式
                 //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

                 //ABP提供了扩展方法PageBy分页方式
                 var taskList = query.PageBy(input).ToList();

                 return new PagedResultDto<ReadyMBillDetailedReportDto>(tasksCount, taskList);

             });

            return resS;
        }
    }
}
