namespace CA.API.Models
{
    public class VMUserAuthorization
    {
        public int ID { get; set; }
        public int MenuID { get; set; }
        public string MenuParent { get; set; }
        public string MenuChild { get; set; }
        public int RightsValue { get; set; }
    }
}
