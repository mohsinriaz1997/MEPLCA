using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class MstUserProfile
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
        public string TokenValue { get; set; }
        public string EncryptKey { get; set; }
        public string DesignationDescription { get; set; }
        public string DepartmentDescription { get; set; }
        public bool? FlgPasswordRequest { get; set; }
    }
}
