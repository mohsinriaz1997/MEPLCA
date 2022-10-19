using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstCostType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool FlgActive { get; set; }
    }
}
