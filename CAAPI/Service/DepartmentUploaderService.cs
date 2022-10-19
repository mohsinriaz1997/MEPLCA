using CA.API.Interfaces.Uploader;
using ExcelDataReader;
using Microsoft.AspNetCore.Components;

namespace CA.API.Service
{
    public class DepartmentUploaderService : IDepartmentUploader
    {
        //[Inject]
        //public IDepartmentMaster _mstDepartment { get; set; }
        private IDepartmentMaster _mstDepartment;
        //private WBCContext _DBContext;
        public DepartmentUploaderService(IDepartmentMaster departmentMaster)
        {
            _mstDepartment = departmentMaster;
        }
        public async Task<IEnumerable<MstDepartment>> ReadFile(IFormFile file)
        {
            
            List<MstDepartment> comp = new List<MstDepartment>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        if (reader.GetValue(0) != null && reader.GetValue(1) != null)
                        {
                            if (reader.GetValue(0).ToString() != "Code" && reader.GetValue(0).ToString() != "Description")
                            {
                                comp.Add(new MstDepartment { Code = reader.GetValue(0).ToString(), Description = reader.GetValue(1).ToString() });
                            }
                        }
                    }
                }

                //foreach (var item in comp)
                //{
                //    var result = await _mstDepartment.Insert(item,'1');

                //}
            }

            return comp;
        }
    }
}
