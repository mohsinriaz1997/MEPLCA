using CA.API.Interfaces.Uploader;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CA.API.Controllers
{
    public class UploaderController : Controller
    {
        private IDepartmentUploader _trnsDirectMaterial;
        public UploaderController(IDepartmentUploader departmentUploader)
        {
            _trnsDirectMaterial= departmentUploader;

        }
        [Route("read-excel-file")]
        [HttpPost]
        public async Task<IActionResult> ReadExcelFile(IFormFile file)
        {
            try
            {
                var result = await _trnsDirectMaterial.ReadFile(file);
                return Ok(new ApiResponse
                {
                    status_code = ((result != null) ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest),
                    Message = result != null ? "company user uploaded" : "failed",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}
