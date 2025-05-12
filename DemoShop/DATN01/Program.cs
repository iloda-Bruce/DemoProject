using DATN01.Areas.Repository;
using DATN01.Data;
using DATN01.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); // Or your preferred distributed cache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
//builder.Services.AddScoped<SessionFilter>();
builder.Services.AddSingleton<ILogin, LoginAction>();
builder.Services.AddSingleton<IProductServices, ProductService>();
var app = builder.Build();

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
#pragma warning disable
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action}/{id?}"); //This is Administrator route. You can you {id} and other parameters which you want
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"); // This route is for Controllers which are situated in project controller folder
});

app.Run();
