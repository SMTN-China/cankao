using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.Entities
{
    public interface IBaseEntities :IAudited, IMustHaveTenant, IPassivable
    {
         
    }
}
