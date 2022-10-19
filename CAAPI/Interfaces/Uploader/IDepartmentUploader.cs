
using Microsoft.AspNetCore.Mvc;

namespace CA.API.Interfaces.Uploader
{
    public interface IDepartmentUploader
    {
        Task<IEnumerable<MstDepartment>> ReadFile(IFormFile file);

    }





}
