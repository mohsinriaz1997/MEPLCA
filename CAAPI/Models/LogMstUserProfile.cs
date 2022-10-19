using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class LogMstUserProfile
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int FkdesignationId { get; set; }
        public int FkdeptId { get; set; }
        public string EmailId { get; set; }
        public bool FlgSuper { get; set; }
        public bool FlgActive { get; set; }
        public string LogUser { get; set; }
        public DateTime LogDate { get; set; }
        public int SourceId { get; set; }
    }
}
