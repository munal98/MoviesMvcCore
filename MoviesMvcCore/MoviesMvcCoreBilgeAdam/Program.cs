using Microsoft.EntityFrameworkCore;
using MoviesMvcCoreBilgeAdam.Contexts;
using MoviesMvcCoreBilgeAdam.Services;
using MoviesMvcCoreBilgeAdam.Services.Bases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IoC Container Kütüphaneleri: Autofac, Ninject
#region IoC Container: Inversion of Control Container

// AddScoped(): istek (request) boyunca objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak) bir kere oluþturulur ve yanýt (response) dönene kadar bu obje hayatta kalýr.
// AddSingleton(): web uygulamasý baþladýðýnda objenin referansýný (genelde interface veya abstract class) kullandýðýmýz yerde obje (somut class'tan oluþturulacak) bir kere oluþturulur ve uygulama çalýþtýðý sürece (IIS üzerinden uygulama durudurulmadýðý veya yeniden baþlatýlmadýðý) sürece bu obje hayatta kalýr.
// AddTransient(): istek (request) baðýmsýz ihtiyaç olan objenin referansýný (genelde interface veya abstract class) kullandýðýmýz her yerde bu objeyi new'ler.

//builder.Services.AddDbContext<MovieContext>();
builder.Services.AddScoped<DbContext, MovieContext>();

builder.Services.AddScoped<IMovieService, MovieService>();

#endregion

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
