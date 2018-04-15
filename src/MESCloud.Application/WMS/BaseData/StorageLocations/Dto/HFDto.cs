using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.WMS.BaseData.StorageLocations.Dto
{
    public class HFDto
    {
        public string LastIP { get; set; }

        public int[] Bus { get; set; }

        public string[] ShelfCode { get; set; }

        public bool MoreSurface { get; set; }

        public int LayerCount { get; set; }

        public string StorageId { get; set; }

        public string StorageLocationTypeId { get; set; }
    }
}
