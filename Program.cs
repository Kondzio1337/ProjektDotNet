using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projekt.Autoryzacja;
using Projekt.Data;
using Projekt.Logic;
using Projekt.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ProjektContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjektContext") ?? throw new InvalidOperationException("Connection string 'ProjektContext' not found.")));
builder.Services.AddAuthentication("CiastkoAuth")
    .AddCookie("CiastkoAuth", options =>
    {
        options.Cookie.Name = "CiastkoAuth";
   });
/*
builder.Services.AddAuthentication("KoszykCiastko")
    .AddCookie("KoszykCiastko", options =>
    {
        options.Cookie.Name = "KoszykCiastko";
        options.Cookie.MaxAge = TimeSpan.FromDays(7);
    });
*/
builder.Services.AddAuthorization(options => {
options.AddPolicy("uzytkownik", policy => policy.RequireClaim("Department", "Uzytkownik"));
options.AddPolicy("kierownik", policy => policy.RequireClaim("Department", "Kierownik"));
    options.AddPolicy("admin", policy => policy.RequireClaim("Department", "Admin"));


});
builder.Services.AddSingleton<IAuthorizationHandler, KierownikHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ShoppingCartActions>();
builder.Services.AddSession();
var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
//app.MapDefaultControllerRoute();
//app.UseAuthorization();

//app.MapRazorPages();


app.Run();
