using Abp.Domain.Entities;
using MESCloud.Entities.WMS.BaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MESCloud.Entities.WMS.ProduceData
{
    public class ReelMoveMethod : Entity<string>, IBaseEntities
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(30)]

        public string Name { get; set; }
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

        public ICollection<RMMStorageMap> OutStorages { get; set; }

        public bool IsActive { get; set; }
        [StringLength(30)]

        public string InStorageId { get; set; }
        public Storage InStorage { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int TenantId { get; set; }

        [NotMapped]
        public ICollection<AllocationType> AllocationTypes{get; set;}
        [StringLength(50)]

        public string AllocationTypesStr
        {
            get
            {
                if (AllocationTypes == null)
                {
                    return null;
                }
                return string.Join("|", AllocationTypes.Select(s => s.ToString()));
            }
            set
            {
                if (value == null)
                {
                    AllocationTypes = null;
                }
                else
                {
                    var res = new List<AllocationType>();
                    var strs = value.Split('|');
                    foreach (var item in strs)
                    {
                        res.Add(Enum.Parse<AllocationType>(item));
                    }
                    AllocationTypes = res;
                }
            }
        }
    }

    public enum AllocationType
    {

        /// <summary>
        /// 转仓
        /// </summary>
        Move = 0,
        /// <summary>
        /// 上架
        /// </summary>
        OnSL,
        /// <summary>
        /// 下架
        /// </summary>
        UpSl,
        /// <summary>
        /// 发料
        /// </summary>
        Send,
        /// <summary>
        /// 退料
        /// </summary>
        Return,
        /// <summary>
        /// 收料
        /// </summary>
        Received,
        /// <summary>
        /// 注册
        /// </summary>
        Register,
        /// <summary>
        /// 发首料
        /// </summary>
        SendFirstReel,
        /// <summary>
        /// 补料
        /// </summary>
        SupplyReel,
        /// <summary>
        /// 库位下架
        /// </summary>
        UpByShelf
    }
}
