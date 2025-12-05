using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;
using TALENTSPHERE.Data;
using Nest;
using TALENTSPHERE.Hubs;
using TALENTSPHERE.Hubs.Additionally;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 1024 * 1024;
});
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Auth/Login"; // куда перенаправлять, если не авторизован
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));

var app = builder.Build();

app.UseCors();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapHub<ChatHub>("chat");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Landing}")
        .WithStaticAssets();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Auth}/{action=Logout}")
//        .WithStaticAssets();

app.Run();

