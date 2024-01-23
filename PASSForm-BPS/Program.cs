using Microsoft.EntityFrameworkCore;
using PASSForm_BPS.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.AspNetCore;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

//commit for checking Muhammad Danish
//Serilog.Log.Logger = new LoggerConfiguration()
//    .WriteTo.MySQL(
//        connectionString: "Server=localhost;port=3307;user=root;Database=pass_db;convert zero datetime=true;", // Replace with your MySQL connection details
//        tableName: "exceptionlogs") // Set your desired table name
//    .CreateLogger();

//IConfiguration configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();
//builder.Services.AddDbContext<PassDbContext>(option => option.UseMySQL(configuration.GetConnectionString("MySqlServerConnection")));


builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

//commit for checking Muhammad Danish
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<PassDbContext>(option => option.UseMySQL(configuration.GetConnectionString("MySqlServerConnection")));

//commit for checking Muhammad Danish
IConfiguration configuration2 = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<TestSalesDbContext>(option => option.UseSqlServer(configuration2.GetConnectionString("SqlConnection")));

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => true; // consent required
//    options.MinimumSameSitePolicy = SameSiteMode.Strict;
//});
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{



//    options.IdleTimeout = TimeSpan.FromDays(1);

//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//    options.Cookie.SameSite = SameSiteMode.None;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//    options.Cookie.Path = "/";
//    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//});

//builder.Services.AddAuthorization();
//    options =>
//{
//    options.DefaultPolicy = new AuthorizationPolicyBuilder("Cookies").RequireAuthenticatedUser().Build();
//});
builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();
//builder.Services.AddHttpClient();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  //  app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseExceptionHandler("/ListView/Error");
app.UseHsts();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   // pattern: "{controller=Home}/{action=Index}/{}");
pattern: "{controller=Login}/{action=Login}/{id?}");
app.Run();
