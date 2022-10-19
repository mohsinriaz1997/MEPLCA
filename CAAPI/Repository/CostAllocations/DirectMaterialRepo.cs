using CA.API.Interfaces.AdministrationData;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CA.API.Repository.CostAllocations
{
    public class DirectMaterialRepo : IDirectMaterial
    {
        private WBCContext _DBContext;

        public DirectMaterialRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsDirectMaterial>> GetAllData()
        {
            List<TrnsDirectMaterial> oList = new List<TrnsDirectMaterial>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsDirectMaterials.Include(t => t.TrnsDirectMaterialDetails).ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<List<TrnsDirectMaterial>> GetAllDataItem(int DocNum)
        {
            List<TrnsDirectMaterial> oList = new List<TrnsDirectMaterial>();
            
            try
            {


                //string sql = @"SELECT d.*,l.* FROM TrnsDirectMaterial d INNER JOIN TrnsDirectMaterialDetail l ON d.ID = l.FKID where d.DocNum=" + DocNum + "";
                //oList = _DBContext.TrnsDirectMaterials.FromSqlRaw(sql).ToList();
                //foreach (var item in oList)
                //{
                //    var Detail = _DBContext.TrnsDirectMaterials.Include(y=>y.TrnsDirectMaterialDetails).Where(x => x.DocNum == item.DocNum).ToList();
                //    _DBContext.TrnsDirectMaterials.RemoveRange(Detail);
                //    break;
                //}
                oList = _DBContext.TrnsDirectMaterials.Include(y => y.TrnsDirectMaterialDetails).Where(x =>  x.DocNum ==  DocNum).ToList();

            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<List<TrnsDirectMaterial>> GetAllData(string ForAnalysis)
        {
            List<TrnsDirectMaterial> oList = new List<TrnsDirectMaterial>();

            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsDirectMaterials.Include(y => y.TrnsDirectMaterialDetails).Where(x => x.ForAnalysis == "" + ForAnalysis + "").ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(TrnsDirectMaterial oTrnsDirectMaterial)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsDirectMaterials.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsDirectMaterial.DocNum = maxDocNum;
                    _DBContext.TrnsDirectMaterials.Add(oTrnsDirectMaterial);
                    _DBContext.SaveChanges();
                    response.Id = 1;
                    response.Message = "Saved successfully";
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
            }
            return response;
        }

        public async Task<ApiResponseModel> Update(TrnsDirectMaterial oTrnsDirectMaterial)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                     var Detail = _DBContext.TrnsDirectMaterialDetails.Where(x => x.Fkid == oTrnsDirectMaterial.Id).ToList();
                    _DBContext.TrnsDirectMaterialDetails.RemoveRange(Detail);
                    _DBContext.TrnsDirectMaterials.Update(oTrnsDirectMaterial);
                    _DBContext.SaveChanges();
                    response.Id = 1;
                    response.Message = "Saved successfully";
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                response.Id = 0;
                response.Message = "Failed to save successfully";
            }
            return response;
        }
    }
}