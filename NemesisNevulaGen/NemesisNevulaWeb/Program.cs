using Microsoft.AspNetCore.Authentication.Cookies;
using NemesisNevulaWeb.Controllers;
using System.Security.Claims;

const int DESARROLLO = 0;
const int PRODUCCION = 1;

// Cambiar el valor de esta variable para cambiar el modo
int modo = PRODUCCION;
WebApplicationBuilder builder;

if(modo == DESARROLLO)
{
    builder = WebApplication.CreateBuilder(args);
}
else
{
    builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        EnvironmentName = Environments.Production
    });
}

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Usuario/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        option.AccessDeniedPath = "/Usuario/Login";
    });

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
