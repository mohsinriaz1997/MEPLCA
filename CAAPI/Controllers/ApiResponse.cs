namespace CA.API.Controllers
{
    internal class ApiResponse
    {
        public int status_code { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
    }
}