using MESCloud.CommonDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Authorization.Accounts.Dto
{
    public class TenantNzSelectDto : IsTenantAvailableOutput
    {
        public string Name { get; set; }

        public string Value { get { return this.TenantId.ToString(); } }

        public string Lable { get { return this.Name; } }
    }
}
