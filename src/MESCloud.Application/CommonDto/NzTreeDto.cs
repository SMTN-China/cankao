using System;
using System.Collections.Generic;
using System.Text;

namespace MESCloud.CommonDto
{
    public class NzTreeDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool Checked { get; set; }
        public bool HalfChecked { get; set; }
        public ICollection<NzTreeDto> Children { get; set; }
        public object Data { get; set; }
    }
}
