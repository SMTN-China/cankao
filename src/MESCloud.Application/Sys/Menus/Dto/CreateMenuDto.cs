using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MESCloud.Entities;

namespace MESCloud.Menus.Dto
{
    [AutoMapTo(typeof(Menu))]
    public class CreateMenuDto : ICustomValidate
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        [StringLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// i18n主键
        /// </summary>
        [StringLength(100)]
        public string Translate { get; set; }
        /// <summary>
        /// 是否为菜单组
        /// </summary>
        public bool Group { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        [StringLength(255)]
        public string Link { get; set; }
        /// <summary>
        /// 外部链接
        /// </summary>
        [StringLength(255)]
        public string ExternalLink { get; set; }
        /// <summary>
        /// 链接 target
        ///  枚举 target?: '_blank' | '_self' | '_parent' | '_top'
        /// </summary>
        [StringLength(10)]
        public string Target { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(50)]
        public string Icon { get; set; }
        /// <summary>
        /// 徽标色。（注：`group:true` 无效）
        /// </summary>
        [StringLength(20)]
        public string Index { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 父级菜单名
        /// </summary>
        [StringLength(20)]
        public string ParentName { get; set; }

        public int? TenantId { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            //string[] target = new string[] { "_blank", "_self", "_parent", "_top" };
            //if (!target.Contains(Target))
            //{
            //    context.Results.Add(new ValidationResult("Target 必须为 [" + string.Join(';', target) + "]中之一。"));
            //    //throw new NotImplementedException("Target 必须为 [" + string.Join(';', target) + "]中之一。");
            //}

        }
    }
}
