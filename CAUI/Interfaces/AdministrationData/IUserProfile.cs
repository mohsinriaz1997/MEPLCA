using CA.API.Models;
using MudBlazor;

namespace CA.UI.Interfaces.AdministrationData
{
    public interface IUserProfile
    {
        Task<List<MstUserProfile>> GetAllData();
        Task<ApiResponseModel> Insert(MstUserProfile oMstUserProfile);
        Task<ApiResponseModel> Update(MstUserProfile oMstUserProfile);
        Task<MstUserProfile> ValidateLogin(MstUserProfile mstUser);
        Task<bool> Logout();

        Task<List<MstMenu>> GetAllMenu();
        Task<List<UserAuthorization>> FetchUserAuth(int userID);
        Task<ApiResponseModel> AddUserAuthorization(List<UserAuthorization> oUserAuthorization);
        Task<ApiResponseModel> GenerateOTP(MstUserProfile oMstUser);
        Task<ApiResponseModel> AuthenticateOTP(PasswordReset oPasswordReset);
        Task<ApiResponseModel> ChangePassword(MstUserProfile oMstUser);

        Task<List<UserDataAccess>> GetAllFormAndCostType(int userID);
        Task<List<UserDataAccess>> GetAllFormAndCostTypesResource(string userID);
        Task<List<UserDataAccess>> GetAllFormAndCostTypesFOHRate(string userID);
        Task<List<UserDataAccess>> GetAllFormAndCostTypesVariableOverHeadCost(string userID);
        Task<List<UserDataAccess>> GetAllFormAndCostTypesDirectMaterial(string userID);
        Task<ApiResponseModel> AddUserDataAccess(List<UserDataAccess> oUserDataAccess);
    }
}