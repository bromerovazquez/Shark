using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.HUB;
using Drim.Interfaces.MVC.Models;

using Drim.Interfaces.MVC.Repositorys;
using Drim.Interfaces.MVC.Services;
using Drim.Interfaces.MVC.ViewModels;
using Drim.Interfaces.MVC.WorkerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Host.UseSerilog();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddSignalR();



var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


builder.Services.AddDbContext<APPDRIMDbContext>(options => options.UseSqlServer(ConnectionCatalogDB.APPDRIM.GetConectionString()));

builder.Services.AddDbContextFactory<DRIMDbContext>(options => options.UseSqlServer(ConnectionCatalogDB.DRIM.GetConectionString()));

builder.Services.AddDbContextFactory<G001Context>(options => options.UseSqlServer(ConnectionCatalogDB.G001.GetConectionString()));

builder.Services.AddDbContextFactory<WebDatosContext>(options => options.UseSqlServer(ConnectionCatalogDB.WEB_DATOS.GetConectionString()));


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddSingleton<IDrimRepository, DrimRespository>();

builder.Services.AddSingleton<IManagerRepository, ManagerRepository>();

builder.Services.AddSingleton<IWebDatosRepository, WebDatosRepository>();

builder.Services.AddSingleton<ITraspasosModel, TraspasosModel>();
builder.Services.AddSingleton<IAlbaranesModel, AlbaranesModel>();
builder.Services.AddSingleton<IBaseModel, BaseModel>();

builder.Services.AddSingleton<IInterfacesArchivosServices, InterfacesArchivosServices>();
//builder.Services.AddScoped<ITestRepos, TestRepos>();
builder.Services.AddHostedService<WorkerInterfaces>();
builder.Services.AddHostedService<WorkerLogsInterfaces>();

builder.Services.AddSession();

builder.Services.AddIdentity<UsuarioDrim, RolesDrim>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.ClaimsIdentity.UserIdClaimType = "UserID";

}).AddEntityFrameworkStores<APPDRIMDbContext>().AddDefaultTokenProviders();



builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "usDrim";
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.IsEssential = true;
});


Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();



Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Account}/{action=Login}/{id?}"
    pattern: "{controller=ManagerInterfaces}/{action=LogsInterfaces}"
    );

app.UseEndpoints(endpoints => {
    endpoints.MapHub<LogInterfacesHub>("/logsinterfaces");
});

app.UseSerilogRequestLogging();


app.Run();
