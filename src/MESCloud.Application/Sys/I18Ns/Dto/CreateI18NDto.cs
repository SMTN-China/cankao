using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MESCloud.Sys.I18Ns.Dto
{
    public class CreateI18NDto
    {
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public string I18NKey { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public int? TenantId { get; set; }
    }
}
