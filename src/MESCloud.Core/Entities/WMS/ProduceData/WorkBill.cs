using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class WorkBill   : Entity<string>, IBaseEntities
    {
        [StringLength(30)]
        public string ProductId { get; set; }

        public MPN Product { get; set; }
        [StringLength(30)]
        public string LineId { get; set; }

        public Line Line { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(50)]

        public string Info { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(200)]
        public string Remark { get; set; }
        public ICollection<ReadyMBillWorkBillMap> ReadyMBills { get; set; }

        /// <summary>
        /// 工单套数
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 备料套数
        /// </summary>
        public int ReadyMQty { get; set; }

        /// <summary>
        /// 生产套数
        /// </summary>
        public int ProductionQty { get; set; }

        public bool IsActive { get; set; }

        public DateTime PlanStartTime { get; set; }

        public DateTime PlanEndTime { get; set; }


        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }        

        public WorkBillStatus WorkBillStatus { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }
       

    }

    public enum WorkBillStatus
    {
        /// <summary>
        /// 新建
        /// </summary>
        New,
        /// <summary>
        /// 分配完成
        /// </summary>
        Assigned,
        /// <summary>
        /// 备料
        /// </summary>
        ReadyM,
        /// <summary>
        /// 备料完成
        /// </summary>
        ReadyMCompletion,
        /// <summary>
        /// 开始生产
        /// </summary>
        ProductionStrat,
        /// <summary>
        /// 生产挂起
        /// </summary>
        ProductionSuspend,
        /// <summary>
        /// 生产完成
        /// </summary>
        ProductionCompletion,
        /// <summary>
        /// 强制完成
        /// </summary>
        ForceCompletion
    }
}
