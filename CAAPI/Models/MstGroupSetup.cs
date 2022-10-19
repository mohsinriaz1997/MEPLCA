using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstGroupSetup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool FlgActive { get; set; }
    }
}
