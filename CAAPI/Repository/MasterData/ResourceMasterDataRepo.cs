using CA.API.Repository.AdministrationData;

namespace CA.API.Repository.MasterData
{
    public class ResourceMasterDataRepo : IResourceMasterData
    {
        private WBCContext _DBContext;
        private IApprovalSetup _approvalSetupRepo;

        public ResourceMasterDataRepo(WBCContext dBContext, IApprovalSetup approvalSetupRepo)
        {
            _DBContext = dBContext;
            _approvalSetupRepo = approvalSetupRepo;
        }

        public async Task<List<MstResource>> GetAllData()
        {
            List<MstResource> oList = new List<MstResource>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstResources.Include(t => t.MstResourceDetails).ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<List<MstResourceDetail>> GetAllDataDyes()
        {
            List<MstResourceDetail> oList = new List<MstResourceDetail>();
            try
            {
                //oList = _DBContext.MstResourceDetails.Where(t => t.TypeOfResr=="Dyes").ToList();
                oList = await (from a in _DBContext.MstResourceDetails
                               where a.TypeOfResr == "Dyes"
                               select a).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<List<MstResourceDetail>> GetAllDataMachine()
        {
            List<MstResourceDetail> oList = new List<MstResourceDetail>();
            try
            {
                oList = await (from a in _DBContext.MstResourceDetails
                               where a.TypeOfResr == "Machine"
                               select a).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<List<MstResourceDetail>> GetAllDataGasoline()
        {
            List<MstResourceDetail> oList = new List<MstResourceDetail>();
            try
            {
                oList = await (from a in _DBContext.MstResourceDetails
                               where a.TypeOfResr == "Gasoline"
                               select a).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<List<MstResourceDetail>> GetAllDataTools()
        {
            List<MstResourceDetail> oList = new List<MstResourceDetail>();
            try
            {
                oList = await (from a in _DBContext.MstResourceDetails
                               where a.TypeOfResr == "Tools"
                               select a).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstResource oMstResource)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    int maxDocNum = _DBContext.MstResources.Max(p => (int?)p.DocNum) ?? 0;
                    maxDocNum++;
                    oMstResource.DocNum = maxDocNum;
                    oMstResource.DocStatus = "Draft";
                    oMstResource.DocApprovalStatus = "Pending";
                    _DBContext.MstResources.Add(oMstResource);
                    _DBContext.SaveChanges();
                    var userCode = _DBContext.MstUserProfiles.Where(x => x.UserCode == oMstResource.CreatedBy).FirstOrDefault();
                    var chkStatus = _approvalSetupRepo.InsertDocApproval(userCode.Id, Convert.ToInt32(oMstResource.DocNum), "flgResourceMasterData", 4, "Rescource Master Data", userCode.UserCode);
                    if (chkStatus == 2)
                    {
                        oMstResource.DocStatus = "Opened";
                        oMstResource.DocApprovalStatus = "Approved";
                        _DBContext.MstResources.Update(oMstResource);
                        _DBContext.SaveChanges();
                        response.Id = 1;
                        response.Message = "Saved successfully";
                    }
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

        public async Task<ApiResponseModel> Update(MstResource oMstResource)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstResources.Update(oMstResource);
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