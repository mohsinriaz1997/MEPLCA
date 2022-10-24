using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CA.API.Repository.AdministrationData
{
    public class UserProfileRepo : IUserProfile
    {
        private WBCContext _DBContext;

        public UserProfileRepo(WBCContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<List<MstUserProfile>> GetAllData()
        {
            List<MstUserProfile> oList = new List<MstUserProfile>();
            try
            {
                await Task.Run(() =>
                {
                    //oList = _DBContext.MstBranchs.Where(a => a.FlgActive == true).ToList();
                    //oList = (from a in _DBContext.MstBranchs
                    //         where a.FlgActive == true
                    //         select a).ToList();
                    oList = _DBContext.MstUserProfiles.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> Insert(MstUserProfile oMstUserProfiles)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstUserProfiles.Add(oMstUserProfiles);
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

        public async Task<ApiResponseModel> Update(MstUserProfile oMstUserProfiles)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    _DBContext.MstUserProfiles.Update(oMstUserProfiles);
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

        public async Task<MstUserProfile> ValidateLogin(string UserCode, string Password)
        {
            MstUserProfile oUser = new MstUserProfile();
            try
            {
                await Task.Run(() =>
                {
                    oUser = _DBContext.MstUserProfiles.Where(x => x.UserCode == UserCode && x.Password == Password).FirstOrDefault();
                });
                if (oUser is not null)
                {
                    oUser.TokenValue = await GenerateToken(oUser);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oUser;
        }

        private async Task<string> GenerateToken(MstUserProfile oUser)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, oUser.UserCode),
                    new Claim(ClaimTypes.NameIdentifier, oUser.Id.ToString()),
                    new Claim(ClaimTypes.Surname, oUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, oUser.EmailId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(5)).ToUnixTimeSeconds().ToString())
                };
                var Token = new JwtSecurityToken(
                    new JwtHeader(
                        new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MEPLWBCkiSecretKeyJWTkliyaBahutSecret")), SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims)
                    );

                string TokenString = "";
                TokenString = new JwtSecurityTokenHandler().WriteToken(Token);
                return TokenString;
            }
            catch (Exception ex)
            {
                return "Authentication Error.";
            }
        }

        public async Task<List<MstMenu>> GetAllMenu()
        {
            List<MstMenu> oList = new List<MstMenu>();
            try
            {
                await Task.Run(() =>
                {
                    oList = _DBContext.MstMenus.ToList();
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<List<UserAuthorization>> FetchUserAuth(int userID)
        {
            List<UserAuthorization> oList = new List<UserAuthorization>();
            try
            {
                await Task.Run(() =>
                {
                    if (userID == 1)
                    {
                        string qry1 = @"SELECT  
                                    ID,
                                    MenuName,
                                    MenuLink,
                                    MenuParent,
                                    ID as FKMenuID,
                                    1 as FKUserID,
                                    2 AS UserRights
                                    ,isnull(CreatedBy,'-') as CreatedBy,isnull(CreatedDate,GetDate()) as CreatedDate,isnull(CAppStamp,'-') as CAppStamp,isnull(UpdatedBy,'-') as UpdatedBy,isnull(UpdatedDate,GetDate()) as UpdatedDate,isnull(UAppStamp,'-') as UAppStamp
                                    FROM MstMenu ";
                        oList = _DBContext.UserAuthorizations.FromSqlRaw(qry1).ToList();
                    }
                    else
                    {
                        string qry = $@"SELECT  
                                t1.ID,
                                T1.MenuName,
                                T1.MenuLink,
                                T1.MenuParent,
                                T2.FKMenuID,
                                T2.FKUserID,
                                isnull(T2.UserRights,1) AS UserRights
                                ,isnull(t2.CreatedBy,'-') as CreatedBy,isnull(t2.CreatedDate,GETDATE()) as CreatedDate,isnull(t2.CAppStamp,'-') as CAppStamp,isnull(t2.UpdatedBy,'-') as UpdatedBy,isnull(t2.UpdatedDate,GETDATE()) as UpdatedDate,isnull(t2.UAppStamp,'-') as UAppStamp
                                FROM MstMenu T1
                                LEFT JOIN UserAuthorization T2 ON T2.FKMenuID = T1.MenuID and T2.FKUserID = @userID";
                        oList = _DBContext.UserAuthorizations.FromSqlRaw(qry, new SqlParameter("@userID", userID)).ToList();
                    }

                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);

            }
            return oList;
        }

        public async Task<ApiResponseModel> AddUserAuthorization(List<UserAuthorization> oUserAuthorization)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    foreach (var item in oUserAuthorization)
                    {
                        var Detail = _DBContext.UserAuthorizations.Where(x => x.FkuserId == item.FkuserId).ToList();
                        _DBContext.UserAuthorizations.RemoveRange(Detail);
                        break;
                    }
                    _DBContext.UserAuthorizations.AddRange(oUserAuthorization);
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

        public async Task<ApiResponseModel> GenerateOTP(MstUserProfile oMstUser)
        {
            MstUserProfile oUser = new MstUserProfile();
            PasswordReset oPassword = new PasswordReset();
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    oUser = _DBContext.MstUserProfiles.Where(x => x.EmailId == oMstUser.EmailId).FirstOrDefault();
                    if (oUser != null)
                    {
                        string keyString = "ShabbirTileUser123456789";
                        var key = Encoding.UTF8.GetBytes(keyString);//16 bit or 32 bit key string

                        using (var aesAlg = Aes.Create())
                        {
                            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                            {
                                using (var msEncrypt = new MemoryStream())
                                {
                                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                                    using (var swEncrypt = new StreamWriter(csEncrypt))
                                    {
                                        swEncrypt.Write(oUser.EmailId);
                                    }

                                    var iv = aesAlg.IV;

                                    var decryptedContent = msEncrypt.ToArray();

                                    var result = new byte[iv.Length + decryptedContent.Length];

                                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                                    Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                                    string EncryptKey = Convert.ToBase64String(result);

                                    oPassword = _DBContext.PasswordResets.Where(x => x.Email == oUser.EmailId && x.FlgActive == true).FirstOrDefault();
                                    if (oPassword == null)
                                    {
                                        oPassword = new PasswordReset();
                                        oPassword.Email = oUser.EmailId;
                                        oPassword.UserCode = oUser.UserCode;
                                        oPassword.EncryptKey = EncryptKey.Substring(0, 10);
                                        oPassword.FlgActive = true;
                                    }

                                    oPassword.FlgActive = false;
                                    _DBContext.PasswordResets.Update(oPassword);
                                    _DBContext.SaveChanges();

                                    // Sending Email
                                    string Subject = "Reset Password Request";
                                    string Detail = "Your One time Password (OTP) is given below;";
                                    string Body = Detail + "<br/><b>" + EncryptKey.Substring(0, 10) + "</b>";

                                    Email oEmail = new Email();
                                    if (oEmail.SentEmail(Subject, Body, "WBC", oPassword.Email))
                                    {
                                        oPassword.Id = 0;
                                        oPassword.FlgActive = true;
                                        oPassword.EncryptKey = EncryptKey.Substring(0, 10);
                                        _DBContext.PasswordResets.Add(oPassword);
                                        _DBContext.SaveChanges();

                                        response.Id = 1;
                                        response.Message = "Email initiated Successfully.";
                                    }
                                    else
                                    {
                                        response.Id = 1;
                                        response.Message = "Failed to initiated email Successfully.";
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        response.Id = 0;
                        response.Message = "Email not found";
                    }
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return response;
        }

        public async Task<ApiResponseModel> AuthenticateOTP(PasswordReset pPassword)
        {
            PasswordReset oPassword = new PasswordReset();
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    oPassword = _DBContext.PasswordResets.Where(x => x.EncryptKey == pPassword.EncryptKey && x.FlgActive == true).FirstOrDefault();
                    if (oPassword != null)
                    {
                        response.Id = 1;
                        response.Message = "OTP authenticate successfully.";
                    }
                    else
                    {
                        response.Id = 0;
                        response.Message = "OTP expired.";
                    }
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return response;
        }

        public async Task<ApiResponseModel> ChangePassword(MstUserProfile pMstUser)
        {
            MstUserProfile oUser = new MstUserProfile();
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    oUser = _DBContext.MstUserProfiles.Where(x => x.EmailId == pMstUser.EmailId && x.FlgActive == true).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.Password = pMstUser.Password;
                        _DBContext.MstUserProfiles.Update(oUser);
                        _DBContext.SaveChanges();
                        response.Id = 1;
                        response.Message = "Password successfully update.";
                    }
                    else
                    {
                        response.Id = 0;
                        response.Message = "Failed to update password.";
                    }
                });
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return response;
        }

        public async Task<List<UserDataAccess>> GetAllFormAndCostType(int UserID)
        {
            List<UserDataAccess> oList = new List<UserDataAccess>();
            try
            {
                using (var command = _DBContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $"select t1.FormCode,t1.FormName,t2.id as FkCostId,t2.Description,isnull(flgAccess,0) as flgAccess from MstForm t1 inner join MstCostType t2 on 1=1 left join UserDataAccess t3 on t3.FormCode=t1.FormCode and t3.FkCostID=t2.ID and FkUserID = {UserID} where flgDataAccess = 1";
                    command.CommandType = CommandType.Text;
                    _DBContext.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            UserDataAccess oData = new UserDataAccess();
                            oData.FormCode = Convert.ToInt32(result["FormCode"].ToString());
                            oData.FormName = result["FormName"].ToString();
                            oData.FkCostId = Convert.ToInt32(result["FkCostId"].ToString());
                            oData.Description = result["Description"].ToString();
                            oData.FlgAccess = Convert.ToBoolean(result["flgAccess"].ToString());
                            oList.Add(oData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesResource(string UserID)
        {
            List<UserDataAccess> oList = new List<UserDataAccess>();
            try
            {
                using (var command = _DBContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @$"select * from UserDataAccess t1 inner join MstUserProfile t2 on t1.FkUserID=t2.ID where t2.UserCode='{UserID}' and t1.FormCode='4' and flgAccess='1' ";
                    command.CommandType = CommandType.Text;
                    _DBContext.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            UserDataAccess oData = new UserDataAccess();
                            oData.FormCode = Convert.ToInt32(result["FormCode"].ToString());
                            oData.FormName = result["FormName"].ToString();
                            oData.FkCostId = Convert.ToInt32(result["FkCostId"].ToString());
                            oData.Description = result["Description"].ToString();
                            oData.FlgAccess = Convert.ToBoolean(result["flgAccess"].ToString());
                            oList.Add(oData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesFOHRate(string UserID)
        {
            List<UserDataAccess> oList = new List<UserDataAccess>();
            try
            {
                using (var command = _DBContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @$"select * from UserDataAccess t1 inner join MstUserProfile t2 on t1.FkUserID=t2.ID where t2.UserCode='{UserID}' and t1.FormCode='7' and flgAccess='1' ";
                    command.CommandType = CommandType.Text;
                    _DBContext.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            UserDataAccess oData = new UserDataAccess();
                            oData.FormCode = Convert.ToInt32(result["FormCode"].ToString());
                            oData.FormName = result["FormName"].ToString();
                            oData.FkCostId = Convert.ToInt32(result["FkCostId"].ToString());
                            oData.Description = result["Description"].ToString();
                            oData.FlgAccess = Convert.ToBoolean(result["flgAccess"].ToString());
                            oList.Add(oData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesVariableOverHeadCost(string UserID)
        {
            List<UserDataAccess> oList = new List<UserDataAccess>();
            try
            {
                using (var command = _DBContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @$"select * from UserDataAccess t1 inner join MstUserProfile t2 on t1.FkUserID=t2.ID where t2.UserCode='{UserID}' and t1.FormCode='8' and flgAccess='1' ";
                    command.CommandType = CommandType.Text;
                    _DBContext.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            UserDataAccess oData = new UserDataAccess();
                            oData.FormCode = Convert.ToInt32(result["FormCode"].ToString());
                            oData.FormName = result["FormName"].ToString();
                            oData.FkCostId = Convert.ToInt32(result["FkCostId"].ToString());
                            oData.Description = result["Description"].ToString();
                            oData.FlgAccess = Convert.ToBoolean(result["flgAccess"].ToString());
                            oList.Add(oData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }
        public async Task<List<UserDataAccess>> GetAllFormAndCostTypesDirectMaterial(string UserID)
        {
            List<UserDataAccess> oList = new List<UserDataAccess>();
            try
            {
                using (var command = _DBContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @$"select * from UserDataAccess t1 inner join MstUserProfile t2 on t1.FkUserID=t2.ID where t2.UserCode='{UserID}' and t1.FormCode='9' and flgAccess='1' ";
                    command.CommandType = CommandType.Text;
                    _DBContext.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            UserDataAccess oData = new UserDataAccess();
                            oData.FormCode = Convert.ToInt32(result["FormCode"].ToString());
                            oData.FormName = result["FormName"].ToString();
                            oData.FkCostId = Convert.ToInt32(result["FkCostId"].ToString());
                            oData.Description = result["Description"].ToString();
                            oData.FlgAccess = Convert.ToBoolean(result["flgAccess"].ToString());
                            oList.Add(oData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
            return oList;
        }

        public async Task<ApiResponseModel> AddUserDataAccess(List<UserDataAccess> oUserDataAccess)
        {
            ApiResponseModel response = new ApiResponseModel();
            try
            {
                await Task.Run(() =>
                {
                    foreach (var item in oUserDataAccess)
                    {
                        var Detail = _DBContext.UserDataAccesses.Where(x => x.FkUserId == item.FkUserId).ToList();
                        _DBContext.UserDataAccesses.RemoveRange(Detail);
                        break;
                    }
                    _DBContext.UserDataAccesses.AddRange(oUserDataAccess);
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