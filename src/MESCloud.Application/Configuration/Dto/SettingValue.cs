using Abp.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Configuration.Dto
{
    public class SettingValue : ISettingValue
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
