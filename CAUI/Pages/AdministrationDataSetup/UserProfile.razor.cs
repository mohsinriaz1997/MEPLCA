using Blazored.LocalStorage;
using CA.API.Models;
using CA.UI.General;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.MasterData;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CA.UI.Pages.AdministrationDataSetup
{
    public partial class UserProfile
    {
        private string LoginUserCode = "";
        private bool Loading = false;
        private bool isShow;
        private string searchString1 = "";
        MudTextField<string> pwField1;

        private bool FilterFunc(MstUserProfile element) => FilterFunc(element, searchString1);

        private InputType PasswordInput = InputType.Password;
        private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        public string TextValue { get; set; }
        private MstDepartment oModel = new MstDepartment();
        private List<MstDepartment> oDepartList = new List<MstDepartment>();
        // Designation

        private MstDesignation oModelDesignation = new MstDesignation();
        private List<MstDesignation> oDesignationtList = new List<MstDesignation>();



        // User Profile
        private MstUserProfile oModelUserProfile = new MstUserProfile();

        private IEnumerable<MstUserProfile> oUserProfileList = new List<MstUserProfile>();

        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public ILocalStorageService _localStorageService { get; set; }
        [Inject]
        public IUserProfile _mstUserProfile { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDepartmenMastert _mstDepartment { get; set; }

        [Inject]
        public IDesignationMaster _mstDesignation { get; set; }

        [Inject]
        public IUserProfile _mstUserProfiled { get; set; }

        private IEnumerable<MstUserProfile> oListUser = new List<MstUserProfile>();

        public IMask AlphaNumericMask = new RegexMask(@"^[a-zA-Z0-9_]*$");

        private async Task<ApiResponseModel> Save()
        {
            try
            {
                var usercode = oModelUserProfile.UserCode.Count();
                var username = oModelUserProfile.UserName.Count();
                var password = oModelUserProfile.Password.Count();
                string email = oModelUserProfile.EmailId.ToLower();
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                //string email = oModelUserProfile.EmailId;
                //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //Match match = regex.Match(email);
                //if (match.Success)
                //    oModelUserProfile.EmailId = email + " is Valid Email Address";
                //else
                //    oModelUserProfile.EmailId = email + " is Invalid Email Address";

                Loading = true;
                var res = new ApiResponseModel();
                await Task.Delay(3);
                if (!string.IsNullOrWhiteSpace(oModelUserProfile.UserCode))
                {
                    if (oUserProfileList.Where(x => x.UserCode == oModelUserProfile.UserCode).Count() > 0)
                    {
                        Snackbar.Add("Code already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;

                    }
                    if (oUserProfileList.Where(x => x.EmailId == oModelUserProfile.EmailId).Count() > 0)
                    {
                        Snackbar.Add("Email already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;
                    }
                    if (oUserProfileList.Where(x => x.UserName == oModelUserProfile.UserName).Count() > 0)
                    {
                        Snackbar.Add("Description already exist", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;
                    }
                    else if (usercode < 3)
                    {
                        Snackbar.Add("User Code Must be greater then 3 ", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;
                    }
                    else if (username < 3)
                    {
                        Snackbar.Add("User Name Must be greater then 3 ", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;
                    }
                    else if (password < 3)
                    {
                        Snackbar.Add("Password Must be greater then 8 ", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;
                    }
                    else if (match.Success == false)
                    {
                        Snackbar.Add("Email  Must be in proper formet ", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                        return res;


                    }
                    else
                    {


                        oModelUserProfile.FkdesignationId = Convert.ToInt32(oModelDesignation.Id);
                        oModelUserProfile.DesignationDescription = oModelDesignation.Description;

                        oModelUserProfile.FkdeptId = Convert.ToInt32(oModel.Id);
                        oModelUserProfile.DepartmentDescription = oModel.Description;
                        oModel.FlgActive = oModel.FlgActive;

                        //oModelUserProfile.FkdeptId = oModel.Id;
                        if (oModelUserProfile.Id == 0)
                        {
                            res = await _mstUserProfiled.Insert(oModelUserProfile);
                        }
                        else
                        {
                            res = await _mstUserProfiled.Update(oModelUserProfile);
                        }
                    }
                    if (res != null && res.Id == 1)
                    {
                        Snackbar.Add(res.Message, Severity.Info, (options) => { options.Icon = Icons.Sharp.Info; });
                        await Task.Delay(3000);
                        Navigation.NavigateTo("/UserProfile", forceLoad: true);
                    }
                    else
                    {
                        Snackbar.Add(res.Message, Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                    }
                    oModel.FlgActive = true;
                    oModelUserProfile.FlgSuper = true;
                }
                else
                {
                    Snackbar.Add("Please fill the required field(s)", Severity.Error, (options) => { options.Icon = Icons.Sharp.Error; });
                }
                Loading = false;
                return res;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
                return null;
            }
        }

        private async Task<IEnumerable<MstDesignation>> SearchCostType(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))
                //    return oDesignationtList.Select(o => new MstDesignation
                //    {
                //        Id = o.Id,
                //        Description = o.Description
                //    }).ToList();
                //var res = oDesignationtList.Where(x => x.FlgActive == true && x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                //return res.Select(x => new MstDesignation
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var designationList = (from a in oDesignationtList
                               where a.FlgActive == true
                               select a
             ).ToList();
                return designationList;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        private async Task<IEnumerable<MstDepartment>> SearchDepartment(string value)
        {
            try
            {
                await Task.Delay(1);
                //if (string.IsNullOrWhiteSpace(value))

                //    return oDepartList.Where(o=>o.FlgActive==true).Select(o => new MstDepartment
                //    {
                //        Id = o.Id,
                //        Description = o.Description,
                //        FlgActive = true


                //    }).ToList();
                //var res = oDepartList.Where(x => x.FlgActive == true && x.Description.ToUpper().Contains(value.ToUpper())).ToList();
                //return res.Where(x=>x.FlgActive=true).Select(x => new MstDepartment
                //{
                //    Id = x.Id,
                //    Description = x.Description
                //}).ToList();
                var dptlist = (from a in oDepartList
                               where a.FlgActive == true
                               select a
                             ).ToList();
                return dptlist;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                return null;
            }
        }
        public void RemoveRecord(int LineNum)
        {
            try
            {
                var res = oUserProfileList.Where(x => x.Id != LineNum);
                oUserProfileList = res;
                if (oUserProfileList.Count() == 0)
                {
                    oModelUserProfile = new MstUserProfile();
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }
        private async Task GetAllUser()
        {
            try
            {
                oUserProfileList = await _mstUserProfiled.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        public void EditRecord(int LineNum)
        {
            //oModelDesignation.Id = oModelUserProfile.FkdesignationId;
            try
            {
                var res = oUserProfileList.Where(x => x.Id == LineNum).FirstOrDefault();
                if (res != null)
                {
                    oModelUserProfile.Id = res.Id;
                    oModelUserProfile.UserCode = res.UserCode;
                    oModelUserProfile.UserName = res.UserName;
                    oModelUserProfile.FlgActive = res.FlgActive;
                    oModelUserProfile.FlgSuper = res.FlgSuper;

                    oModelDesignation.Id = res.FkdesignationId;
                    oModelDesignation.Description = res.DesignationDescription;
                    
                    oModel.Id = res.FkdeptId;
                    oModel.Description = res.DepartmentDescription;
                    

                    //oModelUserProfile.FkdesignationId = res.FkdesignationId;
                    //oModelUserProfile.DrsignationDescription = res.DrsignationDescription;

                    oModelUserProfile.FkdeptId = res.FkdeptId;
                    oModelUserProfile.EmailId = res.EmailId;

                    //oModelUserProfile = oUserProfileList.Where(x => x.Id != LineNum);
                }
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private bool FilterFunc(MstUserProfile element, string searchString1)
        {
            if (string.IsNullOrWhiteSpace(searchString1))
                return true;
            if (element.FlgSuper.Equals(searchString1))
                return true;
            if (element.FlgActive.Equals(searchString1))
                return true;
            return false;
        }

        private void ShowPassword()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
        private IEnumerable<string> PasswordStrength(string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                yield return "Password is required!";
                yield break;
            }
            if (Password.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(Password, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(Password, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(Password, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }

        private async Task GetAllDepartments()
        {
            try
            {
                oDepartList = await _mstDepartment.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        private async Task GetAllDesignation()
        {
            try
            {
                oDesignationtList = await _mstDesignation.GetAllData();
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            //oModelUserProfile.UserCode = "";
            try
            {
                Loading = true;

                await GetAllDesignation();
                await GetAllDepartments();
                await GetAllUser();
                Loading = false;
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
                Loading = false;
            }
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        var Session = await _localStorageService.GetItemAsync<MstUserProfile>("User");
        //        if (Session != null)
        //        {
        //            var res = await _mstUserProfile.FetchUserAuth(Session.Id);
        //            if (res.Where(x => x.MenuName == "Cost Type" && x.UserRights != 1).ToList().Count > 0)
        //            {
        //                LoginUserCode = Session.UserCode;
        //            }
        //            else
        //            {
        //                Navigation.NavigateTo("/Login", forceLoad: true);
        //            }
        //        }
        //        else
        //        {
        //            Navigation.NavigateTo("/Login", forceLoad: true);
        //        }

        //    }
        //}
    }
}