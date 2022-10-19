using CA.API.Models;

namespace CA.API.Repository.CostAllocations
{
    public class SalesQuotationRepo : ISalesQuotation
    {
        private WBCContext _DBContext;

        public SalesQuotationRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<TrnsSalesQuotation>> GetAllData()
        {
            List<TrnsSalesQuotation> oList = new List<TrnsSalesQuotation>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.TrnsSalesQuotations
                    .Include(t => t.TrnsSalesQuotationDetails)
                    .ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        //public async Task<ApiResponseModel> Insert(TrnsSalesQuotation oTrnsSalesQuotation)
        //{
        //    ApiResponseModel response = new ApiResponseModel();
        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            int maxDocNum = _DBContext.TrnsSalesQuotations.Max(p => (int?)p.DocNum) ?? 0;
        //            maxDocNum++;
        //            oTrnsSalesQuotation.DocNum = maxDocNum;
        //            _DBContext.TrnsSalesQuotations.Add(oTrnsSalesQuotation);
        //            _DBContext.SaveChanges();
        //            response.Id = 1;
        //            response.Message = "Saved successfully";
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Logs.GenerateLogs(ex);
        //        response.Id = 0;
        //        response.Message = "Failed to save successfully";
        //    }
        //    return response;
        //}

        public async Task<ApiResponseModel> Insert(InsertSalesQuotation oTrnsSalesQuotation)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.TrnsSalesQuotations.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oTrnsSalesQuotation.oTrnsSalesQuotation.DocNum = maxDocNum;
                    _DBContext.TrnsSalesQuotations.Add(oTrnsSalesQuotation.oTrnsSalesQuotation);
                    var oListDM = oTrnsSalesQuotation.oTrnsDirectMaterial.Where(x => x.ForAnalysis == "Yes" && x.FkSqdocNum == 0).ToList();
                    oListDM.ToList().ForEach(i => i.FkSqdocNum = oTrnsSalesQuotation.oTrnsSalesQuotation.DocNum);
                    _DBContext.TrnsDirectMaterials.AddRange(oListDM);
                    var oListVOC = oTrnsSalesQuotation.oTrnsVoc.Where(x => x.ForAnalysis == "Yes" && x.FkSqdocNum == 0).ToList();
                    oListVOC.ToList().ForEach(i => i.FkSqdocNum = oTrnsSalesQuotation.oTrnsSalesQuotation.DocNum);
                    _DBContext.TrnsVocs.AddRange(oListVOC);
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

        public async Task<ApiResponseModel> Update(InsertSalesQuotation oTrnsSalesQuotation)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    var Detail = _DBContext.TrnsSalesQuotationDetails.Where(x => x.Fkid == oTrnsSalesQuotation.oTrnsSalesQuotation.Id).ToList();
                    _DBContext.TrnsSalesQuotationDetails.RemoveRange(Detail);
                    _DBContext.TrnsSalesQuotations.Update(oTrnsSalesQuotation.oTrnsSalesQuotation);
                    var oListDM = oTrnsSalesQuotation.oTrnsDirectMaterial.Where(x => x.ForAnalysis == "Yes" && x.FkSqdocNum == 0);
                    oListDM.ToList().ForEach(i => i.FkSqdocNum = oTrnsSalesQuotation.oTrnsSalesQuotation.DocNum);
                    _DBContext.TrnsDirectMaterials.AddRange(oListDM);
                    var oListVOC = oTrnsSalesQuotation.oTrnsVoc.Where(x => x.ForAnalysis == "Yes" && x.FkSqdocNum == 0);
                    oListVOC.ToList().ForEach(i => i.FkSqdocNum = oTrnsSalesQuotation.oTrnsSalesQuotation.DocNum);
                    _DBContext.TrnsVocs.AddRange(oListVOC);
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