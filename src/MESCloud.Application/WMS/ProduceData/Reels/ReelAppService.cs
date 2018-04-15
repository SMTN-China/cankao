using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Web.Models;
using AutoMapper;
using Dapper;
using MESCloud.Authorization;
using MESCloud.CommonDto;
using MESCloud.Entities;
using MESCloud.Entities.WMS.BaseData;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.BaseData.BarCodeAnalysiss;
using MESCloud.WMS.BaseData.MPNs.Dto;
using MESCloud.WMS.BaseData.Slots.Dto;
using MESCloud.WMS.BaseData.Storages.Dto;
using MESCloud.WMS.ProduceData.ReceivedReelBills.Dto;
using MESCloud.WMS.ProduceData.Reels.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MESCloud.WMS.ProduceData.Reels
{
    [AbpAuthorize(PermissionNames.Pages_Reels)]
    public class ReelAppService : AsyncCrudAppService<Reel, ReelDto, string, PagedResultRequestMESDto, ReelDto, ReelDto>, IReelAppService
    {
        readonly IRepository<Reel, string> _repository;
        readonly IRepository<ReelMoveLog, string> _repositoryReelMoveLog;
        readonly IRepository<StorageLocation, string> _repositorysl;
        readonly IRepository<Storage, string> _repositorysStorage;
        readonly IRepository<StorageArea, string> _repositorysStorageA;
        readonly IRepository<MPNStorageAreaMap, string> _repositorysMPNA;
        readonly IRepository<MPN, string> _repositorympn;
        readonly IRepository<ReelMoveMethod, string> _repositoryRMM;
        readonly IRepository<Slot> _repositorySlot;
        readonly IBarCodeAnalysisAppService _barCodeAnalysisAppService;
        readonly IRepository<ReadyMBillDetailed, string> _repositoryReadyMBilld;
        readonly IRepository<ReadyMBill, string> _repositoryReadyMBill;
        readonly IRepository<Setting, long> _repositoryT;
        readonly IRepository<ReelSendTemp, string> _repositoryRST;
        readonly IRepository<ReelSupplyTemp, string> _repositoryReelSupplyTemp;
        readonly MSSqlHelper _mSSqlHelper;
        readonly NotificationService NotificationService;
        readonly IRepository<ReadySlot, string> _repositoryReadySlot;

        readonly LightService LightService;
        readonly IRepository<ReceivedReelBill, string> _repositoryrrb;
        public ReelAppService(IRepository<Reel, string> repository,
            IRepository<MPN, string> repositorympn,
            IBarCodeAnalysisAppService barCodeAnalysisAppService,
            IRepository<StorageLocation, string> repositorysl,
            IRepository<ReelSupplyTemp, string> repositoryReelSupplyTemp,
            IRepository<ReelMoveLog, string> repositoryReelMoveLog,
            IRepository<ReadyMBillDetailed, string> repositoryReadyMBilld,
            IRepository<ReceivedReelBill, string> repositoryrrb,
            IRepository<ReadySlot, string> repositoryReadySlot,
            IRepository<ReadyMBill, string> repositoryReadyMBill,
            IRepository<StorageArea, string> repositorysStorageA,
            IRepository<MPNStorageAreaMap, string> repositorysMPNA,
            NotificationService notificationService,
            IRepository<Storage, string> repositorysStorage,
            MSSqlHelper mSSqlHelper,
            IRepository<Slot> repositorySlot,
            IRepository<Setting, long> repositoryT,
            LightService lightService,
            IRepository<ReelSendTemp, string> repositoryRST,
            IRepository<ReelMoveMethod, string> repositoryRMM) : base(repository)
        {
            _repository = repository;
            _barCodeAnalysisAppService = barCodeAnalysisAppService;
            _repositoryRMM = repositoryRMM;
            _repositorympn = repositorympn;
            _repositorysl = repositorysl;
            _repositoryReelMoveLog = repositoryReelMoveLog;
            _repositoryReadyMBilld = repositoryReadyMBilld;
            _repositoryRST = repositoryRST;
            _repositoryReelSupplyTemp = repositoryReelSupplyTemp;
            _repositoryrrb = repositoryrrb;
            _repositorysStorageA = repositorysStorageA;
            _repositoryT = repositoryT;
            _mSSqlHelper = mSSqlHelper;
            _repositoryReadySlot = repositoryReadySlot;
            LightService = lightService;
            _repositoryReadyMBill = repositoryReadyMBill;
            _repositorySlot = repositorySlot;
            _repositorysMPNA = repositorysMPNA;
            _repositorysStorage = repositorysStorage;
            NotificationService = notificationService;

            // NotificationService.Notification("NextFistReel", new NextFistReelDto() { Reel = new ReelDto() { Id = "00000005" } }).Wait();
        }
        [HttpPost]
        public async override Task<PagedResultDto<ReelDto>> GetAll(PagedResultRequestMESDto input)
        {
            var query = MESPagedResult.GetMESPagedResult<Reel>(input, _repository.GetAll());

            var tasksCount = await query.CountAsync();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<ReelDto>(tasksCount, taskList.MapTo<List<ReelDto>>());
        }

        public async Task<ICollection<StorageDto>> GetStorageByKeyName(string keyName)
        {
            var res = await _repositorysStorage.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<Storage>, List<StorageDto>>(res);
        }

        [HttpPost]
        public async Task<PagedResultDto<GroupReelDto>> GetGroupReel(PagedResultRequestMESDto input)
        {
            // 先按参数查询数据
            var query = MESPagedResult.GetMESPagedResult<Reel>(input, _repository.GetAll());

            var res = query.GroupBy(r => new { r.PartNoId, r.StorageId }).Select(r => new GroupReelDto()
            {
                PartNoId = r.Key.PartNoId,
                StorageId = r.Key.StorageId,
                ReelCount = r.Count(),
                TotalQty = r.Sum(reel => reel.Qty)
            });

            var tasksCount = await res.CountAsync();

            var taskList = res.PageBy(input).ToList();

            return new PagedResultDto<GroupReelDto>(tasksCount, taskList);
        }

        public async Task<NextFistReelDto> GetNextFistReel(string readyMId)
        {

            //var nextFistReel = new NextFistReelDto()
            //{
            //    Reel = new ReelDto() { Id = "000001", PartNoId = "pn0001", Qty = 5000, DateCode = "1701", Supplier = "三星", BatchCode = "lot0001" },
            //    Slot = new SlotDto()
            //    {
            //        Qty = 2,
            //        ProductId = "pr0001",
            //        Machine = "cm602-1",
            //        Table = "1",
            //        SlotName = "5",
            //        BoardSide = SideType.T,
            //        Feeder = "0001",
            //        Location = "C24,B56",
            //        LineId = "SMT-B",
            //        Side = SideType.L,
            //        Version = "V-0001"
            //    }
            //};

            //// 推送首料信息
            //await NotificationService.Notification("NextFistReel", nextFistReel);

            //return nextFistReel;


            // 查询备料单信息
            var readyM = _repositoryReadyMBill.FirstOrDefault(readyMId);

            if (readyM == null)
            {
                throw new MesException("当前备料单不存在");
            }

            // 找到发到此备料单且在小车上的物料,有料站表,有库位,是当前备料单


            var reelsInCarByReady = await _repository.GetAll()
                .Where(r => r.ReadyMBillId == readyMId && r.StorageLocationId != null && r.SlotId != null)
                .Include(r => r.Slot)
                .OrderBy(r => (int)r.Slot.BoardSide * -1)
                .ThenBy(r => r.Slot.Index)
                .FirstOrDefaultAsync();

            if (reelsInCarByReady == null)
            {
                throw new MesException("当前备料单首料已备完,或者没有进行首套料挑选");
            }

            // 查询料站表
            var slot = _repositorySlot.Get(reelsInCarByReady.SlotId.Value);

            var nextFistReel = new NextFistReelDto()
            {
                Reel = AutoMapper.Mapper.Map<ReelDto>(reelsInCarByReady),
                Slot = AutoMapper.Mapper.Map<SlotDto>(slot)
            };

            // 推送首料信息
            await NotificationService.Notification("NextFistReel", nextFistReel);

            // 查询库位信息
            var storageLocation = _repositorysl.FirstOrDefault(reelsInCarByReady.StorageLocationId);

            // 亮灯
            LightService.LightOrder(new List<StorageLight>() { new StorageLight() { ContinuedTime = 10, lightOrder = 1, MainBoardId = storageLocation.MainBoardId, RackPositionId = storageLocation.PositionId } });

            // 大灯
            LightService.HouseOrder(new List<HouseLight>() { new HouseLight()
                        { HouseLightSide=1, lightOrder = 1, MainBoardId = storageLocation.MainBoardId } });

            storageLocation.IsBright = true;

            reelsInCarByReady.IsFirstSelected = true;

            return nextFistReel;
        }

        [HttpPost]
        public async Task<PagedResultDto<ReelOutLifeDto>> GetOutLifeReel(PagedResultRequestMESDto input)
        {
            // 查询仓库超期物料
            var overdueDaySet = (await _repositoryT.FirstOrDefaultAsync(c => c.TenantId == AbpSession.TenantId && c.Name == "overdueDay")).Value;

            double overdueDay = double.Parse(overdueDaySet);

            //Logger.Info(overdueDay.ToString());   

            DateTime dt = DateTime.Now.AddDays(overdueDay);

            var outLifeReelsIQ = _repository.GetAllIncluding(r => r.PartNo).Where(r => r.MakeDate <= dt.AddDays((r.ExtendShelfLife + r.PartNo.ShelfLife) * -1) && r.StorageLocationId.Length > 0);

            //var sql = outLifeReelsIQ.ToSql();
            //Logger.Info(outLifeReelsIQ.ToSql());

            // (double)(r.PartNo.ShelfLife + r.ExtendShelfLife)  .AddDays((r.PartNo.ShelfLife + r.ExtendShelfLife) * -1)

            var outLifeReels = await outLifeReelsIQ.ToListAsync();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Reel, ReelOutLifeDto>()
                    .ForMember(x => x.ShelfLife, opt => opt.MapFrom(x => x.PartNo.ShelfLife))
                    .ForMember(x => x.WarningDay, opt => opt.MapFrom(x => overdueDay))
                    .ForMember(x => x.OutLifeDate, opt => opt.MapFrom(x => x.MakeDate.AddDays(x.ExtendShelfLife + x.PartNo.ShelfLife)))
                    .ForMember(x => x.OutLifeDay, opt => opt.MapFrom(x => (DateTime.Now.Date - x.MakeDate.AddDays(x.ExtendShelfLife + x.PartNo.ShelfLife)).Days))
                    .ForMember(x => x.WarnLifeDate, opt => opt.MapFrom(x => x.MakeDate.AddDays(x.ExtendShelfLife + x.PartNo.ShelfLife - overdueDay)))
                    .ForMember(x => x.WarnLifeDay, opt => opt.MapFrom(x => (DateTime.Now.Date - x.MakeDate.AddDays(x.ExtendShelfLife + x.PartNo.ShelfLife - overdueDay)).Days))
                    .ForMember(x => x.OutLifeType, opt => opt.MapFrom(x => (x.MakeDate > DateTime.Now.AddDays((x.ExtendShelfLife + x.PartNo.ShelfLife) * -1)) ? OutLifeType.Warning : OutLifeType.OutLife))
                    ;
            }
               );


            var query = MESPagedResult.GetMESPagedResult<ReelOutLifeDto>(input, config.CreateMapper().Map<List<ReelOutLifeDto>>(outLifeReels).AsQueryable());

            var tasksCount = query.Count();

            //默认的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP提供了扩展方法PageBy分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<ReelOutLifeDto>(tasksCount, taskList);
        }

        public async Task<ReelMoveResDto> ReelMove(ReelMoveDto inputDto)
        {
            // Logger.Info("tm1" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));

            // 获取调拨策略信息
            ReelMoveMethod reelMoveMethod = await _repositoryRMM.GetAll().Where(m => m.Id == inputDto.ReelMoveMethodId).Include(m => m.OutStorages).FirstOrDefaultAsync();

            // 条码解析信息
            ReelDto reelDto = null;

            // 料盘信息
            Reel reel = null;

            // 料盘物料信息
            MPN reelMpn = null;

            // 日志信息
            ReelMoveLog reelMoveLog = new ReelMoveLog() { ReelMoveMethodId = reelMoveMethod.Id };

            // 返回信息
            ReelMoveResDto resDto = new ReelMoveResDto() { IsContinuity = inputDto.IsContinuity, Msg = "操作成功", NextShlefLab = inputDto.ShlefLab };

            // 下架货架信息
            StorageLocation shelfUp = null;

            // 上架货架信息
            StorageLocation shelfOn = null;

            // 条码解析
            Func<Task> BarCodeAnalysis = new Func<Task>(async () =>
            {
                if (reelDto == null)
                {
                    var analysisRes = await _barCodeAnalysisAppService.Analysis(new BaseData.BarCodeAnalysiss.Dto.AnalysisDto() { BarCode = inputDto.BarCode, DtoName = "ReelDto" });
                    if (!analysisRes.Success)
                    {
                        resDto.Msg = analysisRes.Msg;
                        throw new MesException(resDto.Msg);
                    }
                    else
                    {
                        reelDto = analysisRes.Result as ReelDto;
                    }
                }
            });

            // 检查料盘
            Func<Task> CheckReel = new Func<Task>(async () =>
            {
                if (reelDto == null)
                {
                    await BarCodeAnalysis();
                }

                if (reel == null)
                {
                    reel = await _repository.FirstOrDefaultAsync(reelDto.Id);
                    if (reel == null)
                    {
                        throw new MesException(reelDto.Id + "不存在,请先进行料卷注册");
                    }

                    reelMoveLog.ReelId = reel.Id;
                    reelMoveLog.PartNoId = reel.PartNoId;
                    reelMoveLog.Qty = reel.Qty;
                }

            });

            // 检查库位信息
            Func<Task> CheckShelfOn = new Func<Task>(async () =>
            {
                if (shelfOn == null)
                {
                    shelfOn = await _repositorysl.FirstOrDefaultAsync(inputDto.ShlefLab);

                    if (shelfOn == null)
                    {
                        resDto.Msg = "库位[" + inputDto.ShlefLab + "]不存在";
                        throw new MesException(resDto.Msg);
                    }

                    reelMoveLog.StorageLocationId = shelfOn.Id;
                }

            });

            // 获取物料信息
            Func<Task> CheckMPN = new Func<Task>(async () =>
            {
                if (reelMpn == null)
                {
                    reelMpn = await _repositorympn.FirstOrDefaultAsync(reelDto.PartNoId);
                    if (reelMpn == null)
                    {
                        resDto.Msg = "料号[" + reel.PartNoId + "]未维护";
                        throw new MesException(resDto.Msg);
                    }
                }

            });

            // 获取物料库位信息
            Func<Task> GetShelfUp = new Func<Task>(async () =>
            {
                shelfUp = await _repositorysl.FirstOrDefaultAsync(reel.StorageLocationId);
                if (shelfUp == null)
                {
                    resDto.Msg = "料盘[" + reel.Id + "]未上架";
                    throw new MesException(resDto.Msg);
                }
                reelMoveLog.StorageLocationId = shelfUp.Id;
            });

            foreach (var allocationType in reelMoveMethod.AllocationTypes)
            {

                switch (allocationType)
                {
                    case AllocationType.Move:
                    case AllocationType.OnSL:
                    case AllocationType.UpSl:
                    case AllocationType.Send:
                    case AllocationType.Return:
                    case AllocationType.Received:
                    case AllocationType.SendFirstReel:
                    case AllocationType.SupplyReel:
                        if (reel == null)     // 进行料卷检查
                        {
                            await CheckReel();
                        }

                        switch (allocationType)
                        {
                            case AllocationType.Move: // 转仓

                                #region  转仓                             
                                // 检查料卷调出仓是否合法
                                if (!reelMoveMethod.OutStorages.Select(s => s.StorageId).Contains(reel.StorageId))
                                {

                                    resDto.Msg = "料卷不属于该调拨策略的调出仓[" + string.Join('|', reelMoveMethod.OutStorages.Select(s => s.StorageId)) + "]";
                                    throw new MesException(resDto.Msg);
                                }
                                // 进行转仓
                                reel.StorageId = reelMoveMethod.InStorageId;

                                #endregion
                                break;
                            case AllocationType.OnSL: // 上架

                                #region 上架
                                if (shelfOn == null)
                                {
                                    await CheckShelfOn(); // 检查库位信息
                                }

                                // 库位是否有料
                                if (shelfOn.ReelId != null)
                                {
                                    resDto.Msg = "库位[" + inputDto.ShlefLab + "]已绑定料卷[" + shelfOn.ReelId + "]";
                                    throw new MesException(resDto);
                                }

                                // 库位是否在转入仓
                                if (shelfOn.StorageId != reelMoveMethod.InStorageId)
                                {
                                    resDto.Msg = "库位[" + inputDto.ShlefLab + "]不不属于[" + reelMoveMethod.InStorageId + "]仓";
                                    throw new MesException(resDto.Msg);
                                }

                                // 料卷是否上架
                                if (reel.StorageLocationId != null)
                                {
                                    resDto.Msg = "料卷[" + reel.Id + "]已经上架[" + reel.StorageLocationId + "]";
                                    throw new MesException(resDto.Msg);
                                }

                                // 查询物料可用区域

                                var mpnA = _repositorysMPNA.GetAll().Where(r => r.MPNId == reel.PartNoId).ToArray();

                                if (mpnA.Length > 0 && !mpnA.Select(r => r.StorageAreaId).Contains(shelfOn.StorageAreaId))
                                {
                                    resDto.Msg = "物料[" + reel.PartNoId + "]允许上架分区为[" + string.Join('|', mpnA.Select(r => r.StorageAreaId)) + "]";
                                    throw new MesException(resDto);
                                }
                                else
                                {

                                    if (shelfOn.StorageAreaId != null && !mpnA.Select(r => r.StorageAreaId).Contains(shelfOn.StorageAreaId))
                                    {
                                        resDto.Msg = "分区[" + shelfOn.StorageAreaId + "]不允许物料[" + reel.PartNoId + "]上架";
                                        throw new MesException(resDto);
                                    }
                                }
                                // Logger.Info("tm91" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
                                // 寻找下一个空库位
                                var nextShelf = await _repositorysl.GetAll()
                                    .Where(s =>
                                        s.MainBoardId == shelfOn.MainBoardId &&
                                        s.PositionId > shelfOn.PositionId &&
                                        (s.ReelId == null || s.ReelId.Length == 0))
                                    .OrderBy(s => s.PositionId)
                                    .FirstOrDefaultAsync();
                                // Logger.Info("tm92" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
                                if (nextShelf != null)
                                {
                                    resDto.NextShlefLab = nextShelf.Id;
                                }
                                else
                                {
                                    resDto.IsContinuity = false;
                                }

                                // 进行上架双向绑定
                                reel.StorageLocationId = shelfOn.Id;
                                shelfOn.ReelId = reel.Id;

                                // 保存上架信息 
                                // _repositorysl.Update(shelfOn); 
                                #endregion
                                break;
                            case AllocationType.UpSl: // 下架

                                #region 下架
                                await GetShelfUp();
                                // 清除双向绑定
                                shelfUp.ReelId = null;
                                reel.StorageLocationId = null;
                                #endregion
                                break;
                            case AllocationType.Send: // 发料

                                #region 发料
                                // 检测发料临时表里面有没有该数据                
                                var sendtemp = await _repositoryRST.FirstOrDefaultAsync(reel.Id);
                                if (sendtemp == null)
                                {
                                    resDto.Msg = "料卷未被挑料";
                                    throw new MesException(resDto.Msg);
                                }
                                // 查询挑料料站表行数据
                                var readySlot = await _repositoryReadySlot.FirstOrDefaultAsync(r => r.ReReadyMBillId == sendtemp.ReReadyMBillId && r.SlotId == sendtemp.SlotId);
                                if (sendtemp.IsSend)
                                {

                                    resDto.Msg = @"料卷已经发料    " + sendtemp.FisrtStorageLocationId + "" + "\r\n站位信息: \r\n" + "面别: [" + (readySlot.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + readySlot.Machine + "]\r\nTable: [" + readySlot.Table + "]\r\n站位: [" +
                                    readySlot.SlotName + "]\r\n边别: [" + (readySlot.Side == SideType.L ? "L" : "R") + "]";
                                    throw new MesException(resDto.Msg);
                                }

                                // 查询挑料明细行数据
                                var readyBillD = await _repositoryReadyMBilld.FirstOrDefaultAsync(sendtemp.ReadyMBillDetailedId);

                                // 改变发料状态
                                sendtemp.IsSend = true;

                                // 添加发料数量
                                readyBillD.SendQty += reel.Qty;



                                // 料站表发料数量改变
                                readySlot.SendQty += reel.Qty;

                                // 改变料盘备料关联
                                reel.ReadyMBillDetailedId = readyBillD.Id;
                                reel.ReadyMBillId = readyBillD.ReadyMBillId;

                                // 改变物料料站表绑定
                                reel.SlotId = sendtemp.SlotId;

                                // 改变日志备料关联
                                reelMoveLog.ReadyMBillDetailedId = readyBillD.Id;
                                reelMoveLog.ReadyMBillId = readyBillD.ReadyMBillId;
                                reelMoveLog.SlotId = sendtemp.SlotId;

                                // 灭灯
                                await GetShelfUp();
                                shelfUp.IsBright = false;

                                // 查询当前物料的站位信息
                                resDto.Msg = "站位信息: \r\n" + "面别: [" + (readySlot.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + readySlot.Machine + "]\r\nTable: [" + readySlot.Table + "]\r\n站位: [" +
                                    readySlot.SlotName + "]\r\n边别: [" + (readySlot.Side == SideType.L ? "L" : "R") + "]";


                                // 如果为首料进行小车闪灯
                                if (sendtemp.FisrtStorageLocationId != null && sendtemp.FisrtStorageLocationId.Length > 0)
                                {
                                    var shelfF = await _repositorysl.FirstOrDefaultAsync(sendtemp.FisrtStorageLocationId);
                                    StorageLight storage = new StorageLight()
                                    {
                                        ContinuedTime = 10,
                                        lightOrder = 2,
                                        MainBoardId = shelfF.MainBoardId,
                                        RackPositionId = shelfF.PositionId
                                    };
                                    LightService.LightOrder(new List<StorageLight>() { storage });
                                }

                                // 灭小灯和塔灯
                                LightService.LightOrder(new List<StorageLight>() { new StorageLight() { ContinuedTime = 10, lightOrder = 0, MainBoardId = shelfUp.MainBoardId, RackPositionId = shelfUp.PositionId } });

                                CurrentUnitOfWork.SaveChanges();
                                // 大灯 可能需要修改
                                var lights = _repositorysl.GetAll().Where(s => s.MainBoardId == shelfUp.MainBoardId && s.IsBright);
                                if (lights.Count() == 0)
                                {
                                    LightService.HouseOrder(new List<HouseLight>() { new HouseLight()
                                    { HouseLightSide=1, lightOrder = 0, MainBoardId = shelfUp.MainBoardId } });
                                }

                                #endregion
                                break;
                            case AllocationType.Return: // 退料
                                break;
                            case AllocationType.Received: // 收料

                                #region 收料
                                var receiveId = await _repositoryrrb.FirstOrDefaultAsync(r => r.PartNoId == reel.PartNoId && r.Qty > r.ReceivedQty && r.IQCCheckId == inputDto.IQCCheckId);

                                if (receiveId == null)
                                {
                                    resDto.Msg = "当前ERP没有足够的IQC检验单,请确认";
                                    throw new MesException(resDto);
                                }

                                receiveId.ReceivedQty += reel.Qty;
                                if (receiveId.ReceivedQty == receiveId.Qty)
                                {
                                    receiveId.IsActive = true;
                                }
                                reel.ReceivedId = receiveId.Id;
                                reelMoveLog.ReceivedReelBillId = receiveId.Id;
                                #endregion
                                break;
                            case AllocationType.Register: // 注册
                                break;
                            case AllocationType.SendFirstReel: // 发首料
                                break;
                            case AllocationType.SupplyReel: // 补料
                                #region 补料
                                // 检测补料料临时表里面有没有该数据                
                                var supplytemp = await _repositoryReelSupplyTemp.FirstOrDefaultAsync(reel.Id);
                                if (supplytemp == null)
                                {
                                    resDto.Msg = "料卷未被挑料";
                                    throw new MesException(resDto.Msg);
                                }

                                if (supplytemp.IsSend)
                                {
                                    resDto.Msg = @"料卷已经发料    " + supplytemp.FisrtStorageLocationId;
                                    throw new MesException(resDto.Msg);
                                }

                                // 查询挑料明细行数据
                                var readyBillDSupply = await _repositoryReadyMBilld.FirstOrDefaultAsync(supplytemp.ReadyMBillDetailedId);

                                // 改变发料状态
                                supplytemp.IsSend = true;

                                // 添加发料数量
                                readyBillDSupply.SendQty += reel.Qty;

                                // 查询挑料料站表行数据
                                var readySlotSupply = await _repositoryReadySlot.FirstOrDefaultAsync(r => r.ReReadyMBillId == supplytemp.ReReadyMBillId && r.SendPartNoId == reel.PartNoId);

                                // 料站表发料数量改变
                                readySlotSupply.SendQty += reel.Qty;

                                // 改变料盘备料关联
                                reel.ReadyMBillDetailedId = readyBillDSupply.Id;
                                reel.ReadyMBillId = readyBillDSupply.ReadyMBillId;

                                // 改变物料料站表绑定
                                reel.SlotId = supplytemp.SlotId;

                                // 改变日志备料关联
                                reelMoveLog.ReadyMBillDetailedId = readyBillDSupply.Id;
                                reelMoveLog.ReadyMBillId = readyBillDSupply.ReadyMBillId;
                                reelMoveLog.SlotId = supplytemp.SlotId;
                                await GetShelfUp();
                                // 灭灯
                                shelfUp.IsBright = false;

                                resDto.Msg = "站位信息: \r\n" + "面别: [" + (readySlotSupply.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + readySlotSupply.Machine + "]\r\nTable: [" + readySlotSupply.Table + "]\r\n站位: [" +
                                    readySlotSupply.SlotName + "]\r\n边别: [" + (readySlotSupply.Side == SideType.L ? "L" : "R") + "]";

                                // 灭小灯和塔灯
                                LightService.LightOrder(new List<StorageLight>() { new StorageLight() { ContinuedTime = 10, lightOrder = 0, MainBoardId = shelfUp.MainBoardId, RackPositionId = shelfUp.PositionId } });

                                CurrentUnitOfWork.SaveChanges();
                                // 大灯 可能需要修改
                                var lightssupply = _repositorysl.GetAll().Where(s => s.MainBoardId == shelfUp.MainBoardId && s.IsBright);
                                if (lightssupply.Count() == 0)
                                {
                                    LightService.HouseOrder(new List<HouseLight>() { new HouseLight()
                                    { HouseLightSide=1, lightOrder = 0, MainBoardId = shelfUp.MainBoardId } });
                                }

                                #endregion
                                break;
                            case AllocationType.UpByShelf: // 按库位下架
                                break;
                            default:
                                break;
                        }


                        break;
                    case AllocationType.UpByShelf:

                        #region 按库位下架
                        // 找到库位物料
                        var shelfL = _repositorysl.FirstOrDefault(inputDto.BarCode);
                        if (shelfL == null)
                        {
                            throw new MesException("库位[" + inputDto.BarCode + "]不存在");
                        }

                        if (shelfL.ReelId == null)
                        {
                            throw new MesException("库位[" + inputDto.BarCode + "]上无料");
                        }

                        // 查询库位上 reel
                        reel = _repository.FirstOrDefault(shelfL.ReelId);
                        shelfL.ReelId = null;
                        reel.StorageLocationId = null;
                        reelMoveLog.ReelId = reel.Id;
                        reelMoveLog.PartNoId = reel.PartNoId;
                        reelMoveLog.Qty = reel.Qty;
                        #endregion
                        break;
                    case AllocationType.Register:

                        #region  料卷注册
                        // 查询料号是否已经维护
                        await BarCodeAnalysis();

                        reel = _repository.FirstOrDefault(reelDto.Id);

                        if (reel == null)
                        {
                            await CheckMPN();
                            // 刚注册物料不能被禁用
                            reelDto.IsActive = true;
                            reel = reelDto.MapTo<Reel>();

                            // 直接进入注册仓库
                            reel.StorageId = reelMpn.RegisterStorageId;
                            reel = await _repository.InsertAsync(reel);


                            // 进行注册,注册后立马保存料盘信息
                            await CurrentUnitOfWork.SaveChangesAsync();
                        }

                        reelMoveLog.ReelId = reel.Id;
                        reelMoveLog.PartNoId = reel.PartNoId;
                        reelMoveLog.Qty = reel.Qty;
                        #endregion
                        break;
                    default:
                        break;
                }


            }

            // 最后插入调拨日志
            await _repositoryReelMoveLog.InsertAsync(reelMoveLog);

            resDto.Reel = reel.MapTo<ReelDto>();

            #region MyRegion
            //// 条码解析
            //var reelDtoObj = await _barCodeAnalysisAppService.Analysis(new BaseData.BarCodeAnalysiss.Dto.AnalysisDto() { BarCode = inputDto.BarCode, DtoName = "ReelDto" });
            //// Logger.Info("tm2" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));

            //if (!reelDtoObj.Success)
            //{

            //    // 看看是不是扫描到库位了,仅针对下架
            //    if (reelMoveMethod.AllocationTypes.Count == 2 && reelMoveMethod.AllocationTypes.Contains(AllocationType.Move) && reelMoveMethod.AllocationTypes.Contains(AllocationType.UpSl))
            //    {
            //        var shelfL = _repositorysl.FirstOrDefault(inputDto.BarCode);
            //        if (shelfL == null)
            //        {
            //            resDto.Msg = reelDtoObj.Msg;
            //            throw new MesException(resDto);
            //        }

            //        reelDto.Id = _repositorysl.FirstOrDefault(inputDto.BarCode).ReelId;
            //    }
            //    else
            //    {
            //        resDto.Msg = reelDtoObj.Msg;
            //        throw new MesException(resDto);
            //    }
            //}
            //else
            //{
            //    // Logger.Info("tm3" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
            //    reelDto = reelDtoObj.Result as ReelDto;
            //}

            //// 获取数据库的料盘信息
            //Reel reel = _repository.FirstOrDefault(reelDto.Id);
            //// Logger.Info("tm4" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));

            //// 日志记录
            //ReelMoveLog reelMoveLog = new ReelMoveLog();

            //// 查询库位
            //var shelf = _repositorysl.FirstOrDefault(inputDto.ShlefLab);
            //// Logger.Info("tm5" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));


            //StorageLocation shelfOld = new StorageLocation();
            //StorageLocation shelfFirst = new StorageLocation();
            //try
            //{
            //    shelfOld = _repositorysl.FirstOrDefault(reel.StorageLocationId);
            //    // Logger.Info("tm6" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));


            //}
            //catch (Exception)
            //{

            //}

            //Slot reelSlot = new Slot();

            //try
            //{
            //    if (reelMoveMethod.AllocationTypes.Contains(AllocationType.Send) || reelMoveMethod.AllocationTypes.Contains(AllocationType.SendFirstReel))
            //    {
            //        reelSlot = await _repositorySlot.FirstOrDefaultAsync(reel.SlotId.Value);
            //        // Logger.Info("tm7" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));

            //    }
            //}
            //catch (Exception)
            //{


            //}



            //// Logger.Info("tm8" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));

            //if (reelMoveMethod == null)
            //{
            //    resDto.Msg = "未找到调拨策略[" + inputDto.ReelMoveMethodId + "]";
            //    throw new MesException(resDto);
            //}

            //if (reel == null && !reelMoveMethod.AllocationTypes.Contains(AllocationType.Register))
            //{
            //    resDto.Msg = "调拨策略[" + inputDto.ReelMoveMethodId + "]不允许进行料卷注册";
            //    throw new MesException(resDto);
            //}
            //else if (reel == null)
            //{
            //    // 查询料号是否已经维护
            //    var pn = await _repositorympn.FirstOrDefaultAsync(reelDto.PartNoId);
            //    if (pn == null)
            //    {
            //        resDto.Msg = "物料编号[" + reelDto.PartNoId + "]未维护";
            //        throw new MesException(resDto);
            //    }

            //    // 刚注册物料不能被禁用
            //    reelDto.IsActive = true;

            //    // 进行注册
            //    reel = await _repository.InsertAsync(reelDto.MapTo<Reel>());
            //    await CurrentUnitOfWork.SaveChangesAsync();
            //    // Logger.Info("tm81" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
            //}


            //// 数量是否大于0
            //if (reel.Qty < 1)
            //{
            //    resDto.Msg = "料卷数量不能为0";
            //    throw new MesException(resDto);
            //}


            //// 是否已经禁用
            //if (!reel.IsActive)
            //{
            //    resDto.Msg = "料卷以被禁用";
            //    throw new MesException(resDto);
            //}

            ////// 是否过期
            ////if (DateTime.Now >= reel.MakeDate.AddDays(pn.ShelfLife + reel.ExtendShelfLife))
            ////{
            ////    resDto.Msg = "料卷已经超期,到期时间[" + reel.MakeDate.AddDays(pn.ShelfLife + reel.ExtendShelfLife).ToString("yyyy-MM-dd") + "]";
            ////    throw new MesException(resDto);
            ////}

            //// 是否在用
            //if (reel.IsUseed)
            //{
            //    resDto.Msg = "料卷处于使用状态";
            //    throw new MesException(resDto);
            //}

            //// 收料校验 包含收料，且物料没有收料单
            //if (!inputDto.IsReturnReel && reelMoveMethod.AllocationTypes.Contains(AllocationType.Received) && reel.ReceivedId == null)
            //{
            //    // 查询适合当前物料的最早收料单，并收料。直到该盘全部收料
            //    var receiveId = await _repositoryrrb.FirstOrDefaultAsync(r => r.PartNo == reel.PartNo && reel.IsActive && r.Qty > r.ReceivedQty && r.IQCCheckId == inputDto.IQCCheckId);

            //    if (receiveId == null)
            //    {
            //        resDto.Msg = "当前ERP没有足够的IQC检验单,请确认";
            //        throw new MesException(resDto);
            //    }

            //    receiveId.ReceivedQty += reel.Qty;

            //    _repositoryrrb.Update(receiveId);
            //    // CurrentUnitOfWork.SaveChanges();
            //}


            //// 会有发料进行发料
            //if (reelMoveMethod.AllocationTypes.Contains(AllocationType.Send))
            //{
            //    // 检测发料临时表里面有没有该数据                
            //    var sendtemp = await _repositoryRST.FirstOrDefaultAsync(reel.Id);
            //    if (sendtemp == null)
            //    {
            //        resDto.Msg = "料卷未被挑料";
            //        throw new MesException(resDto.Msg);
            //    }

            //    if (sendtemp.IsSend)
            //    {
            //        resDto.Msg = @"料卷已经发料    " + sendtemp.FisrtStorageLocationId;
            //        throw new MesException(resDto.Msg);
            //    }

            //    // 查询挑料明细行数据
            //    var readyBillD = await _repositoryReadyMBilld.FirstOrDefaultAsync(sendtemp.ReadyMBillDetailedId);

            //    // 改变发料状态
            //    sendtemp.IsSend = true;

            //    // 添加发料数量
            //    readyBillD.SendQty += reel.Qty;

            //    // 查询挑料料站表行数据
            //    var readySlot = await _repositoryReadySlot.FirstOrDefaultAsync(r => r.ReReadyMBillId == sendtemp.ReReadyMBillId && r.SlotId == sendtemp.SlotId);

            //    // 料站表发料数量改变
            //    readySlot.SendQty += reel.Qty;

            //    // 改变料盘备料关联
            //    reel.ReadyMBillDetailedId = readyBillD.Id;
            //    reel.ReadyMBillId = readyBillD.ReadyMBillId;

            //    // 改变物料料站表绑定
            //    reel.SlotId = sendtemp.SlotId;

            //    // 改变日志备料关联
            //    reelMoveLog.ReadyMBillDetailedId = readyBillD.Id;
            //    reelMoveLog.ReadyMBillId = readyBillD.ReadyMBillId;
            //    reelMoveLog.SlotId = sendtemp.SlotId;

            //    // 灭灯
            //    shelfOld.IsBright = false;

            //    if (reelSlot != null)
            //    {
            //        // 查询当前物料的站位信息

            //        resDto.Msg = "站位信息: \r\n" + "面别: [" + (reelSlot.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + reelSlot.Machine + "]\r\nTable: [" + reelSlot.Table + "]\r\n站位: [" +
            //            reelSlot.SlotName + "]\r\n边别: [" + (reelSlot.Side == SideType.L ? "L" : "R") + "]";
            //    }

            //    // 如果为首料进行小车闪灯
            //    if (sendtemp.FisrtStorageLocationId != null && sendtemp.FisrtStorageLocationId.Length > 0)
            //    {
            //        shelfFirst = await _repositorysl.FirstOrDefaultAsync(sendtemp.FisrtStorageLocationId);
            //    }

            //}

            //// 会有补料进行补料
            //if (reelMoveMethod.AllocationTypes.Contains(AllocationType.SupplyReel))
            //{
            //    // 检测补料料临时表里面有没有该数据                
            //    var sendtemp = await _repositoryReelSupplyTemp.FirstOrDefaultAsync(reel.Id);
            //    if (sendtemp == null)
            //    {
            //        resDto.Msg = "料卷未被挑料";
            //        throw new MesException(resDto.Msg);
            //    }

            //    if (sendtemp.IsSend)
            //    {
            //        resDto.Msg = @"料卷已经发料    " + sendtemp.FisrtStorageLocationId;
            //        throw new MesException(resDto.Msg);
            //    }

            //    // 查询挑料明细行数据
            //    var readyBillD = await _repositoryReadyMBilld.FirstOrDefaultAsync(sendtemp.ReadyMBillDetailedId);

            //    // 改变发料状态
            //    sendtemp.IsSend = true;

            //    // 添加发料数量
            //    readyBillD.SendQty += reel.Qty;

            //    // 查询挑料料站表行数据
            //    var readySlot = await _repositoryReadySlot.FirstOrDefaultAsync(r => r.ReReadyMBillId == sendtemp.ReReadyMBillId && r.SlotId == sendtemp.SlotId);

            //    // 料站表发料数量改变
            //    readySlot.SendQty += reel.Qty;

            //    // 改变料盘备料关联
            //    reel.ReadyMBillDetailedId = readyBillD.Id;
            //    reel.ReadyMBillId = readyBillD.ReadyMBillId;

            //    // 改变物料料站表绑定
            //    reel.SlotId = sendtemp.SlotId;

            //    // 改变日志备料关联
            //    reelMoveLog.ReadyMBillDetailedId = readyBillD.Id;
            //    reelMoveLog.ReadyMBillId = readyBillD.ReadyMBillId;
            //    reelMoveLog.SlotId = sendtemp.SlotId;

            //    // 灭灯
            //    shelfOld.IsBright = false;

            //    if (reelSlot != null)
            //    {
            //        // 查询当前物料的站位信息

            //        resDto.Msg = "站位信息: \r\n" + "面别: [" + (reelSlot.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + reelSlot.Machine + "]\r\nTable: [" + reelSlot.Table + "]\r\n站位: [" +
            //            reelSlot.SlotName + "]\r\n边别: [" + (reelSlot.Side == SideType.L ? "L" : "R") + "]";
            //    }
            //    else
            //    {
            //        reelSlot = new Slot();
            //        resDto.Msg = "站位信息: \r\n" + "面别: [" + (reelSlot.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + reelSlot.Machine + "]\r\nTable: [" + reelSlot.Table + "]\r\n站位: [" +
            //            reelSlot.SlotName + "]\r\n边别: [" + (reelSlot.Side == SideType.L ? "L" : "R") + "]";
            //    }

            //    //// 如果为首料进行小车闪灯
            //    //if (sendtemp.FisrtStorageLocationId != null && sendtemp.FisrtStorageLocationId.Length > 0)
            //    //{
            //    //    shelfFirst = await _repositorysl.FirstOrDefaultAsync(sendtemp.FisrtStorageLocationId);
            //    //}
            //}


            //// 会有下架操作,进行下架
            //if (reelMoveMethod.AllocationTypes.Contains(AllocationType.UpSl))
            //{
            //    if (shelfOld != null)
            //    {
            //        shelfOld.ReelId = null;
            //    }
            //    reel.StorageLocationId = null;

            //}


            //// 会有上架进行上架校验
            //if (reelMoveMethod.AllocationTypes.Contains(AllocationType.OnSL))
            //{
            //    // 库位不存在
            //    if (shelf == null)
            //    {
            //        resDto.Msg = "库位[" + inputDto.ShlefLab + "]不存在";
            //        throw new MesException(resDto);
            //    }

            //    // 库位是否在转入仓
            //    if (shelf.StorageId != reelMoveMethod.InStorageId)
            //    {
            //        resDto.Msg = "库位[" + inputDto.ShlefLab + "]不不属于[" + reelMoveMethod.InStorageId + "]仓";
            //        throw new MesException(resDto);
            //    }

            //    // 料卷是否上架
            //    if (reel.StorageLocationId != null && reel.StorageId == reelMoveMethod.InStorageId)
            //    {
            //        resDto.Msg = "料卷[" + reel.Id + "]已经上架[" + reel.StorageLocationId + "]";
            //        throw new MesException(resDto);
            //    }

            //    // 查询物料可用区域
            //    var mpnA = _repositorysMPNA.GetAll().Where(r => r.MPNId == reel.PartNoId).ToArray();

            //    if (mpnA.Length > 0 && !mpnA.Select(r => r.StorageAreaId).Contains(shelf.StorageAreaId))
            //    {
            //        resDto.Msg = "物料[" + reel.PartNoId + "]允许上架分区为[" + string.Join('|', mpnA.Select(r => r.StorageAreaId)) + "]";
            //        throw new MesException(resDto);
            //    }
            //    else
            //    {

            //        if (shelf.StorageAreaId != null && !mpnA.Select(r => r.StorageAreaId).Contains(shelf.StorageAreaId))
            //        {
            //            resDto.Msg = "分区[" + shelf.StorageAreaId + "]不允许物料[" + reel.PartNoId + "]上架";
            //            throw new MesException(resDto);
            //        }
            //    }
            //    // Logger.Info("tm91" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
            //    // 寻找下一个空库位
            //    var nextShelf = await _repositorysl.GetAll()
            //        .Where(s =>
            //            s.MainBoardId == shelf.MainBoardId &&
            //            s.PositionId > shelf.PositionId &&
            //            (s.ReelId == null || s.ReelId.Length == 0))
            //        .OrderBy(s => s.PositionId)
            //        .FirstOrDefaultAsync();
            //    // Logger.Info("tm92" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
            //    if (nextShelf != null)
            //    {
            //        resDto.NextShlefLab = nextShelf.Id;
            //    }
            //    else
            //    {
            //        resDto.IsContinuity = false;
            //    }

            //    // 库位是否有料
            //    if (shelf.ReelId != null && shelf.ReelId != "")
            //    {
            //        resDto.Msg = "库位[" + inputDto.ShlefLab + "]已绑定料卷[" + shelf.ReelId + "]";
            //        throw new MesException(resDto);
            //    }

            //    // 进行上架
            //    reel.StorageLocationId = shelf.Id;
            //    shelf.ReelId = reel.Id;

            //    // 如果不包含发料,则清除备料单信息

            //    if (!reelMoveMethod.AllocationTypes.Contains(AllocationType.Send))
            //    {
            //        reel.ReadyMBillId = null;
            //        reel.ReadyMBillDetailedId = null;
            //        reel.SlotId = null;
            //    }

            //    // 保存上架信息
            //    _repositorysl.Update(shelf);
            //}



            //// 转仓判定
            //if (reelMoveMethod.AllocationTypes.Contains(AllocationType.Move))
            //{

            //    // 检查料卷调出仓是否合法
            //    if (!reelMoveMethod.OutStorages.Select(s => s.StorageId).Contains(reel.StorageId))
            //    {

            //        resDto.Msg = "料卷不属于该调拨策略的调出仓[" + string.Join('|', reelMoveMethod.OutStorages.Select(s => s.StorageId)) + "]";
            //        throw new MesException(resDto);
            //    }
            //    // 进行转仓
            //    reel.StorageId = reelMoveMethod.InStorageId;
            //}

            //// 日志
            //reelMoveLog.ReelId = reel.Id;
            //reelMoveLog.PartNoId = reel.PartNoId;
            //reelMoveLog.ReelMoveMethodId = reelMoveMethod.Id;
            //reelMoveLog.Qty = reel.Qty;
            //reelMoveLog.StorageLocationId = shelf == null ? null : shelf.Id;

            //// 更新料卷信息
            //_repository.Update(reel);
            //// Logger.Info("tm9" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));
            //// 插入日志
            //_repositoryReelMoveLog.Insert(reelMoveLog);
            //// Logger.Info("tm10" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff"));

            //resDto.Reel = reel.MapTo<ReelDto>();



            //// 会有首套料发料进行首套料发料
            //if (reelMoveMethod.AllocationTypes.Contains(AllocationType.SendFirstReel))
            //{
            //    // 判断当前物料是否拿对                
            //    if (!reel.IsFirstSelected)
            //    {
            //        resDto.Msg = string.Format("扫描料盘未备首套料选中");

            //        throw new MesException(resDto);
            //    }

            //    // 灭灯
            //    if (shelfOld != null)
            //    {
            //        shelfOld.IsBright = false;

            //    }

            //    if (reelSlot != null)
            //    {
            //        // 查询当前物料的站位信息

            //        resDto.Msg = "站位信息: \r\n" + "面别: [" + (reelSlot.BoardSide == SideType.B ? "S" : "C") + "]\r\n机器: [" + reelSlot.Machine + "]\r\nTable: [" + reelSlot.Table + "]\r\n站位: [" +
            //            reelSlot.SlotName + "]\r\n边别: [" + (reelSlot.Side == SideType.L ? "L" : "R") + "]";
            //    }
            //    reel.IsFirstSelected = false;
            //    //try
            //    //{
            //    //    var res = await GetNextFistReel(reel.ReadyMBillId);
            //    //}
            //    //catch (MesException ex)
            //    //{
            //    //    if (shelfOld != null)
            //    //    {
            //    //        shelfOld.IsBright = false;
            //    //    }

            //    //    resDto.Msg += "\r\n" + ex.Message;
            //    //}
            //    // CurrentUnitOfWork.SaveChanges();
            //}
            //// 灭灯


            //try
            //{
            //    if (reelMoveMethod.AllocationTypes.Contains(AllocationType.Send) || reelMoveMethod.AllocationTypes.Contains(AllocationType.SendFirstReel))
            //    {
            //        // 小灯
            //        //await _notificationService.SendNotification("LightOrder", new List<StorageLight>() { new StorageLight() { ContinuedTime = 10, lightOrder = 0, MainBoardId = shelfOld.MainBoardId, RackPositionId = shelfOld.PositionId } });

            //        LightService.LightOrder(new List<StorageLight>() { new StorageLight() { ContinuedTime = 10, lightOrder = 0, MainBoardId = shelfOld.MainBoardId, RackPositionId = shelfOld.PositionId } });

            //        // CurrentUnitOfWork.SaveChanges();  // 保存数据库变更，再检查大灯


            //        // Thread.Sleep(5000);
            //        CurrentUnitOfWork.SaveChanges();
            //        // 大灯
            //        var lights = _repositorysl.GetAll().Where(s => s.MainBoardId == shelfOld.MainBoardId && s.IsBright);
            //        if (lights.Count() == 0)
            //        {
            //            //    await _notificationService.SendNotification("HouseOrder", new List<HouseLight>() { new HouseLight()
            //            //{ HouseLightSide=0, lightOrder = 0, MainBoardId = shelfOld.MainBoardId } });
            //            LightService.HouseOrder(new List<HouseLight>() { new HouseLight()
            //            { HouseLightSide=shelfOld.PositionId>300? 0:1, lightOrder = 0, MainBoardId = shelfOld.MainBoardId } });
            //        }

            //        if (shelfFirst != null)
            //        {
            //            LightService.LightOrder(new List<StorageLight>() { new StorageLight() { ContinuedTime = 10, lightOrder = 2, MainBoardId = shelfFirst.MainBoardId, RackPositionId = shelfFirst.PositionId } });
            //        }

            //    }
            //}
            //catch (Exception)
            //{


            //}


            // Logger.Info("tm11" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss ffff")); 
            #endregion
            return resDto;
        }

        public async Task UpdateReelESL(UpdateReelESLDto updateReelESL)
        {
            foreach (var reelId in updateReelESL.ReelIds)
            {
                var reel = _repository.FirstOrDefault(reelId);
                if (reel != null)
                {
                    reel.ExtendShelfLife += updateReelESL.AddDay;

                    await _repository.UpdateAsync(reel);
                }
            }

        }

        public async Task<ICollection<MPNDto>> GetPartNoByKeyName(string keyName)
        {
            var res = await _repositorympn.GetAll().Where(c => c.Id.Contains(keyName)).Take(10).ToListAsync();
            return Mapper.Map<List<MPN>, List<MPNDto>>(res);
        }

        public async Task BrightByPartNoIds(LightOrderDto[] input)
        {
            // 查询料号库存
            List<string> reelIds = new List<string>();
            foreach (var item in input)
            {
                var reelsOne = _repository.GetAll().Where(r => r.PartNoId == item.ReelOrPns && r.StorageId == item.StorageId).Select(r => r.Id).ToList();
                reelIds.AddRange(reelsOne);
            }

            var lights = await _repositorysl.GetAll().Where(r => reelIds.Contains(r.ReelId)).GroupBy(r => new StorageLight
            {
                RackPositionId = r.PositionId,
                ContinuedTime = 10,
                lightOrder = 1,
                MainBoardId = r.MainBoardId
            }).Select(r => r.Key).ToListAsync();

            // 小灯
            LightService.LightOrder(lights);


            // 灯塔
            var mains = lights.GroupBy(r => new AllLight { lightOrder = 0, MainBoardId = r.MainBoardId }).Select(r => r.Key).ToList();

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 1, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 1, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }

        public async Task BrightByReelIds(LightOrderDto[] input)
        {

            List<string> reelIds = new List<string>();
            foreach (var item in input)
            {
                var reelsOne = _repository.GetAll().Where(r => r.Id == item.ReelOrPns && r.StorageId == item.StorageId).Select(r => r.Id).ToList();
                reelIds.AddRange(reelsOne);
            }

            // 查询料号库存
            // var reels = _repository.GetAll().Where(r => input.ReelOrPns.Contains(r.Id) && r.StorageId == input.StorageId).Select(r => r.Id).ToList();

            var lights = await _repositorysl.GetAll().Where(r => reelIds.Contains(r.ReelId)).GroupBy(r => new StorageLight
            {
                RackPositionId = r.PositionId,
                ContinuedTime = 10,
                lightOrder = 1,
                MainBoardId = r.MainBoardId
            }).Select(r => r.Key).ToListAsync();

            // 小灯
            LightService.LightOrder(lights);


            // 灯塔
            var mains = lights.GroupBy(r => new AllLight { lightOrder = 0, MainBoardId = r.MainBoardId }).Select(r => r.Key).ToList();

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 1, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 1, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }

        public async Task ExtinguishedByPartNoIds(LightOrderDto[] input)
        {
            // 查询料号库存
            List<string> reelIds = new List<string>();
            foreach (var item in input)
            {
                var reelsOne = _repository.GetAll().Where(r => r.PartNoId == item.ReelOrPns && r.StorageId == item.StorageId).Select(r => r.Id).ToList();
                reelIds.AddRange(reelsOne);
            }

            var lights = await _repositorysl.GetAll().Where(r => reelIds.Contains(r.ReelId)).GroupBy(r => new StorageLight
            {
                RackPositionId = r.PositionId,
                ContinuedTime = 10,
                lightOrder = 0,
                MainBoardId = r.MainBoardId
            }).Select(r => r.Key).ToListAsync();

            // 小灯
            LightService.LightOrder(lights);


            // 灯塔
            var mains = lights.GroupBy(r => new AllLight { lightOrder = 0, MainBoardId = r.MainBoardId }).Select(r => r.Key).ToList();

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }

        public async Task ExtinguishedByReelIds(LightOrderDto[] input)
        {
            // 查询料号库存
            List<string> reelIds = new List<string>();
            foreach (var item in input)
            {
                var reelsOne = _repository.GetAll().Where(r => r.Id == item.ReelOrPns && r.StorageId == item.StorageId).Select(r => r.Id).ToList();
                reelIds.AddRange(reelsOne);
            }

            var lights = await _repositorysl.GetAll().Where(r => reelIds.Contains(r.ReelId)).GroupBy(r => new StorageLight
            {
                RackPositionId = r.PositionId,
                ContinuedTime = 10,
                lightOrder = 0,
                MainBoardId = r.MainBoardId
            }).Select(r => r.Key).ToListAsync();

            // 小灯
            LightService.LightOrder(lights);


            // 灯塔
            var mains = lights.GroupBy(r => new AllLight { lightOrder = 0, MainBoardId = r.MainBoardId }).Select(r => r.Key).ToList();

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 0 }).ToList());

            LightService.HouseOrder(lights.Select(r => new HouseLight() { lightOrder = 0, MainBoardId = r.MainBoardId, HouseLightSide = 1 }).ToList());
        }

        [HttpPost]
        public async Task<GetReceivedsResult> GetReceiveds(ReelMoveDto inputDto)
        {
            var res = new GetReceivedsResult() { Msg = "OK", ReceivedReelBills = new List<ReceivedReelBillDto>() };


            // 条码解析
            var reelDtoObj = await _barCodeAnalysisAppService.Analysis(new BaseData.BarCodeAnalysiss.Dto.AnalysisDto() { BarCode = inputDto.BarCode, DtoName = "ReelDto" });
            if (!reelDtoObj.Success)
            {
                res.Msg = reelDtoObj.Msg;
                throw new MesException(res.Msg);
            }
            ReelDto reel = reelDtoObj.Result as ReelDto;

            // 查询ERP收料单
            var receiveIds = _mSSqlHelper.Connection.Query<ReceivedReelBillDto>(@"SELECT
  id_received as Id,
  ltrim(rtrim(receive_no)) AS ReceivedId,
  ltrim(rtrim(inspect_no)) as IQCCheckId,
  ltrim(rtrim(po_no)) as PoId,
  ltrim(rtrim(part_no)) as PartNoId,
  qty_iqc as Qty
FROM
    vw_rcv_iqc_info where ltrim(rtrim(part_no))=@PartNoId", new { reel.PartNoId }).GroupBy(r => new
            {
                r.PartNoId,
                r.IQCCheckId,
                r.ReceivedId
            }).Select(r => new ReceivedReelBill()
            {
                ReceivedId = r.Key.ReceivedId,
                IQCCheckId = r.Key.IQCCheckId,
                PartNoId = r.Key.PartNoId,
                IsActive = false,
                Qty = r.Sum(rs => rs.Qty),
                PoId = string.Join('|', r.Select(rs => rs.PoId)),
            }).ToList();


            if (receiveIds == null || receiveIds.Count == 0)
            {
                res.Msg = "当前ERP没有足够的IQC检验单,请确认";
                throw new MesException(res.Msg);
            }

            foreach (var receiveId in receiveIds)
            {
                //查询当前收料单是否同步过
                var receiveIdNow = _repositoryrrb.FirstOrDefault(r => r.PartNoId == reel.PartNoId && r.IQCCheckId == receiveId.IQCCheckId);
                if (receiveIdNow == null)
                {
                    res.ReceivedReelBills.Add(_repositoryrrb.Insert(receiveId).MapTo<ReceivedReelBillDto>());
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
                else
                {
                    if (receiveId.Qty != receiveIdNow.Qty)
                    {
                        receiveIdNow.Qty = receiveId.Qty;
                        receiveIdNow.PoId = receiveId.PoId;
                    }
                    if (receiveIdNow.Qty > receiveIdNow.ReceivedQty)
                    {
                        res.ReceivedReelBills.Add(receiveIdNow.MapTo<ReceivedReelBillDto>());
                    }
                }

            }

            if (res.ReceivedReelBills.Count == 0)
            {
                res.Msg = "当前ERP没有足够的IQC检验单,请确认";
                throw new MesException(res.Msg);
            }


            return res;
        }

        public async Task ReturnReel(ReturnReelDto returnReel)
        {
            // 查询备料单是否存在
            var readyMBill = _repositoryReadyMBill.FirstOrDefault(returnReel.ReadyMBillId);

            if (readyMBill == null)
            {
                throw new MesException(string.Format("备料单{0}不存在", returnReel.ReadyMBillId));
            }

            if (readyMBill.ReReadyMBillId == null)
            {
                throw new MesException(string.Format("备料单{0}未进行备料", returnReel.ReadyMBillId));

            }

            // 查询记账备料单下面是否有该料号
            var readyMBillDetaileds = await _repositoryReadyMBilld.GetAll().Where(r => _repositoryReadyMBill.GetAll()
             .Where(r1 => r1.ReReadyMBillId == readyMBill.ReReadyMBillId)
             .Select(r1 => r1.Id).Contains(r.ReadyMBillId) && r.PartNoId == returnReel.PartNoId).ToListAsync();

            if (readyMBillDetaileds == null || readyMBillDetaileds.Count == 0)
            {
                throw new MesException(string.Format("备料单{0}及其合并的备料单不需要物料{1}", returnReel.ReadyMBillId, returnReel.PartNoId));
            }

            // 查询是否发料
            if (readyMBillDetaileds.Sum(r => r.SendQty) == 0)
            {
                throw new MesException(string.Format("备料单{0}及其合并的备料单物料{1}还未发料", returnReel.ReadyMBillId, returnReel.PartNoId));
            }

            // 进行退料
            var readyMBillDetailed = readyMBillDetaileds.FirstOrDefault();
            readyMBillDetailed.ReturnQty += returnReel.Qty;
        }
    }
}
