using CA.API.Models;

namespace CA.API.Repository.CostAllocations
{
    public class VariableOverheadCostRepo : IVariableOverheadCost
    {
        private WBCContext _DBContext;

        public VariableOverheadCostRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsVoc>> GetAllData()
        {
            List<TrnsVoc> oList = new List<TrnsVoc>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsVocs
                    .Include(t => t.TrnsVocactivityDetails)
                    .Include(t => t.TrnsVocmachineDetails)
                    .Include(t => t.TrnsVoclaborDetails)
                    .Include(t => t.TrnsVocelectricityDetails)
                    .Include(t => t.TrnsVocdyesAndMoldDetails)
                    .Include(t => t.TrnsVoctoolsDetails)
                    .Include(t => t.TrnsVocgasolineDetails)
                    .ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(TrnsVoc oTrnsVoc)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsVocs.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsVoc.DocNum = maxDocNum;
                    _DBContext.TrnsVocs.Add(oTrnsVoc);
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

        public async Task<ApiResponseModel> Update(TrnsVoc oTrnsVoc)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    var ActivityDetail = _DBContext.TrnsVocactivityDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVocactivityDetails.RemoveRange(ActivityDetail);
                    var MachineDetail = _DBContext.TrnsVocmachineDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVocmachineDetails.RemoveRange(MachineDetail);
                    var LaborDetail = _DBContext.TrnsVoclaborDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVoclaborDetails.RemoveRange(LaborDetail);
                    var ElectricityDetail = _DBContext.TrnsVocelectricityDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVocelectricityDetails.RemoveRange(ElectricityDetail);
                    var DyesAndMoldDetail = _DBContext.TrnsVocdyesAndMoldDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVocdyesAndMoldDetails.RemoveRange(DyesAndMoldDetail);
                    var ToolsDetail = _DBContext.TrnsVoctoolsDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVoctoolsDetails.RemoveRange(ToolsDetail);
                    var GasolineDetail = _DBContext.TrnsVocdyesAndMoldDetails.Where(x => x.Fkid == oTrnsVoc.Id).ToList();
                    _DBContext.TrnsVocdyesAndMoldDetails.RemoveRange(GasolineDetail);
                    _DBContext.TrnsVocs.Update(oTrnsVoc);
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