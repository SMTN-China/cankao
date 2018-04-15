using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MESCloud.Entities.WMS.ProduceData;
using MESCloud.WMS.ProduceData.ReelMoveMethods.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.WMS.ProduceData.ReadyMBills.Dto
{
    [AutoMapFrom(typeof(ReadyMBill))]
    public class ReadyMBillDto : IEntityDto<string>
    {
        public string Id { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(200)]
        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        public string Remark { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public string Productstr { get; set; }

        /// <summary>
        /// 工单数量列表
        /// </summary>
        public string WorkBilQtys { get; set; }

        public string Linestr { get; set; }
        public ICollection<ReadyMBillWorkBillMapDto> WorkBills { get; set; }
        public DateTime DeliverTime { get; set; }
        /// <summary>
        /// 备料方式
        /// </summary>
        public MakeDetailsType MakeDetailsType { get; set; }
        public string ReelMoveMethodId { get; set; }
        public ReelMoveMethodDto ReelMoveMethod { get; set; }

        public string ReReadyMBillId { get; set; }
        //public ReadyMBillDto ReReadyMBill { get; set; }
        /// <summary>
        /// 备料类型
        /// </summary>
        public ReadyMType ReadyMType { get; set; }
        /// <summary>
        /// 备料时长
        /// </summary>
        public int ReadyMTime { get; set; }
        public bool IsUrgent { get; set; }

        public int Priority { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }

        public ICollection<ReadyMBillDetailedDto> ReadyMBillDetailed { get; set; }

        public DateTime CreationTime { get; set; }
    }


}
