namespace CA.API.Interfaces.AdministrationData
{
    public interface IUserProfile
    {
        Task<List<MstUserProfile>> GetAllData();
        Task<ApiResponseModel> Insert(MstUserProfile oMstUserProfile);
        Task<ApiResponseModel> Update(MstUserProfile oMstUserProfile);
        Task<MstUserProfile> ValidateLogin(string UserName, string Password);
        Task<List<MstMenu>> GetAllMenu();
        Task<List<UserAuthorization>> FetchUserAuth(int userID);
        Task<ApiResponseModel> AddUserAuthorization(List<UserAuthorization> oUserAuthorization);
        Task<ApiResponseModel> GenerateOTP(MstUserProfile oMstUser);
        Task<ApiResponseModel> AuthenticateOTP(PasswordReset oPasswordReset);
        Task<ApiResponseModel> ChangePassword(MstUserProfile oMstUser);
        Task<List<UserDataAccess>> GetAllFormAndCostType(int UserID);
        Task<List<UserDataAccess>> GetAllFormAndCostTypesResource(string UserID);
        Task<List<UserDataAccess>> GetAllFormAndCostTypesFOHRate(string UserID);
        Task<ApiResponseModel> AddUserDataAccess(List<UserDataAccess> oUserDataAccess);
    }
}