global using CA.API.General;
global using CA.API.Interfaces.AdministrationData;
global using CA.API.Interfaces.CostAllocations;
global using CA.API.Interfaces.MasterData;
global using CA.API.Interfaces.SAPData;
global using CA.API.Models;
global using CA.API.Repository.AdministrationData;
global using CA.API.Repository.CostAllocations;
global using CA.API.Repository.MasterData;
global using CA.API.Repository.SAPData;
global using Microsoft.EntityFrameworkCore;
using CA.API;
using CA.API.Custom_Middleware;
using CA.API.Interfaces.Uploader;
using CA.API.Service;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string? DBCon = builder.Configuration.GetSection("APIBaseUrl").ToString();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", JwtOptions =>
{
    JwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MEPLWBCkiSecretKeyJWTkliyaBahutSecret")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(30)
    };
});
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddDbContext<WBCContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IUserProfile, UserProfileRepo>();
builder.Services.AddScoped<IActivityType, ActivityTypeRepo>();
builder.Services.AddScoped<IDepartmentMaster, DepartmentMasterRepo>();
builder.Services.AddScoped<IDesignationMaster, DesignationMasterRepo>();
builder.Services.AddScoped<IItemPriceList, ItemPriceListRepo>();
builder.Services.AddScoped<ISalesPriceList, MstSalesPriceListRepo>();
builder.Services.AddScoped<IResourceMasterData, ResourceMasterDataRepo>();
builder.Services.AddScoped<IGroupSetupMaster, GroupSetupMasterRepo>();
builder.Services.AddScoped<ICostType, CostTypeRepo>();
builder.Services.AddScoped<ICostDriversType, CostDriversTypeRepo>();
builder.Services.AddScoped<ISAPData, SAPDataRepo>();
builder.Services.AddScoped<ILabourRate, LabourRateRepo>();
builder.Services.AddScoped<IDirectMaterial, DirectMaterialRepo>();
builder.Services.AddScoped<IVariableOverheadCost, VariableOverheadCostRepo>();
builder.Services.AddScoped<IMonthlyVOHCalc, MonthlyVOHCalcRepo>();
builder.Services.AddScoped<IFOHRatesCalc, FOHRatesCalcRepo>();
builder.Services.AddScoped<IMontlyFOHCostCalc, MontlyFOHCostCalcRepo>();
builder.Services.AddScoped<IMonthlyFOHDriverRatesCalc, MonthlyFOHDriverRatesCalcRepo>();
builder.Services.AddScoped<ISalesQuotation, SalesQuotationRepo>();
builder.Services.AddScoped<IDepartmentUploader, DepartmentUploaderService>();
builder.Services.AddScoped<IStage, StageRepo>();
builder.Services.AddScoped<IApprovalSetup, ApprovalSetupRepo>();
builder.Services.AddScoped<ISalesQuotation, SalesQuotationRepo>();

Settings.TitleConfig = builder.Configuration.GetValue<string>("TitleConfig");
Settings.EmailConfig = builder.Configuration.GetValue<string>("EmailConfig");
Settings.PasswordConfig = builder.Configuration.GetValue<string>("PasswordConfig");
Settings.HostConfig = builder.Configuration.GetValue<string>("HostConfig");
Settings.PortConfig = builder.Configuration.GetValue<int>("PortConfig");
Settings.IsSSlConfig = builder.Configuration.GetValue<bool>("IsSSlConfig");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();