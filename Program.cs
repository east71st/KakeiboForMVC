using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KakeiboForMVC.Models;
using KakeiboForMVC.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<KakeiboForMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KakeiboForMVCContext") ?? throw new InvalidOperationException("Connection string 'KakeiboForMVCContext' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    KakeiboSeedData.Initialize(services);
    HimokuSeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Kakeibo}/{action=Index}/{id?}");

app.Run();
