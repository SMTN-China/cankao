using Abp.AutoMapper;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.BOMs.Dto
{
    [AutoMapFrom(typeof(BOM))]
    public class CreateBOMDto
    {
        /// <summary>
        /// 机种ID
        /// </summary>
        public string ProductId { get; set; }


        /// <summary>
        /// 物料ID
        /// </summary>
        public string PartNoId { get; set; }


        /// <summary>
        /// 需求数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 允许超发
        /// </summary>
        public bool AllowableMoreSend { get; set; }
        /// <summary>
        /// 超发百分比
        /// </summary>
        public double MoreSendPercentage { get; set; }

        public bool IsActive { get; set; }
    }
}
