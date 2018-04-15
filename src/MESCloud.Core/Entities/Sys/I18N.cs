using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Entities
{
    public class I18N : Entity, IAudited,IMayHaveTenant, IPassivable
    {
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string ClassName { get; set; }
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string PropertyName { get; set; }
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string I18NKey { get; set; }
        [StringLength(30)]
        [DefaultValue("")]
        [Required]
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public long? CreatorUserId { get ; set ; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get; set; }       
        public int? TenantId { get; set; } 
    }
}
