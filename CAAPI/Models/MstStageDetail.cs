using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstStageDetail
    {
        public int Id { get; set; }
        public int? FkStageId { get; set; }
        public int? FkUserId { get; set; }
        public bool? FlgType { get; set; }

        public virtual MstStage FkStage { get; set; }
    }
}
