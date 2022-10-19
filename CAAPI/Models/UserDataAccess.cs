using System;
using System.Collections.Generic;

namespace CA.API.Models
{
    public partial class UserDataAccess
    {
        public int Id { get; set; }
        public int? FormCode { get; set; }
        public string FormName { get; set; }
        public int? FkCostId { get; set; }
        public string Description { get; set; }
        public int? FkUserId { get; set; }
        public bool? FlgAccess { get; set; }
    }
}
