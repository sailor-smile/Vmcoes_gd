using System;
using System.Collections.Generic;

namespace Vmcoes_gd.Enties
{
    public partial class ConstructionSite
    {
        public string ConstCode { get; set; }
        public string ConstName { get; set; }
        public string ConstAddr { get; set; }
        public short? Status { get; set; }
        public int? SortNo { get; set; }
    }
}
