using Blazored.LocalStorage;
using CA.UI;
using CA.UI.Authentication;
using CA.UI.Data.AdministrationData;
using CA.UI.Data.CostAllocation;
using CA.UI.Data.MasterData;
using CA.UI.Data.SAPData;
using CA.UI.Interfaces.AdministrationData;
using CA.UI.Interfaces.Cost_Allocation;
using CA.UI.Interfaces.MasterData;
using CA.UI.Interfaces.SAPData;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddScoped<IDepartmenMastert, MstDepartmentService>();
builder.Services.AddScoped<IUserProfile, MstUserProfileService>();
builder.Services.AddScoped<ICostType, MstCostTypeService>();
builder.Services.AddScoped<IGroupSetupMaster, MstGroupSetupService>();
builder.Services.AddScoped<ICostType, MstCostTypeService>();
builder.Services.AddScoped<ICostDriversType, MstCostDriveService>();
builder.Services.AddScoped<IActivityType, MstActivityTypeService>();
builder.Services.AddScoped<IResourceMasterData, MstResourceService>();
builder.Services.AddScoped<IDesignationMaster, MstDesignationService>();
builder.Services.AddScoped<ISAPData, SAPDataService>();
builder.Services.AddScoped<ISalePriceList, MstSalePriceListService>();
builder.Services.AddScoped<IItemPriceList, MstItemPriceListService>();
builder.Services.AddScoped<ILaborRate, MstLaborRateService>();
builder.Services.AddScoped<IDirectMaterial, MstDirectMaterialService>();
builder.Services.AddScoped<IFOHCostCalc, MstFOHCostCalc>();
builder.Services.AddScoped<IFOHRatesCalc, MstFOHRatesCalc>();
builder.Services.AddScoped<IMonthlyFohDriverRatesCalculation, MstMonthlyFohDriverRatesCalculationService>();
builder.Services.AddScoped<IMonthlyVOHCalculation, MstMonthlyVOHCalculation>();
builder.Services.AddScoped<IVOC, MstVOCCalc>();
builder.Services.AddScoped<IStage, MstStageService>();
builder.Services.AddScoped<IApprovalSetup, MstApprovalSetupService>();
builder.Services.AddScoped<ISalesQuotation, SalesQuotationService>();

//Configuration Start

Settings.APIBaseURL = builder.Configuration.GetValue<string>("APIBaseUrl");

//Configuration End

// Configure the HTTP request pipeline.
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();