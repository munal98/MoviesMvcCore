using Microsoft.EntityFrameworkCore;
using MoviesMvcCoreBilgeAdam.Contexts;
using MoviesMvcCoreBilgeAdam.Services;
using MoviesMvcCoreBilgeAdam.Services.Bases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IoC Container Kütüphaneleri: Autofac, Ninject
#region IoC Container: Inversion of Control Container

// AddScoped(): istek (request) boyunca objenin referansını (genelde interface veya abstract class) kullandığımız yerde obje (somut class'tan oluşturulacak) bir kere oluşturulur ve yanıt (response) dönene kadar bu obje hayatta kalır.
// AddSingleton(): web uygulaması başladığında objenin referansını (genelde interface veya abstract class) kullandığımız yerde obje (somut class'tan oluşturulacak) bir kere oluşturulur ve uygulama çalıştığı sürece (IIS üzerinden uygulama durudurulmadığı veya yeniden başlatılmadığı) sürece bu obje hayatta kalır.
// AddTransient(): istek (request) bağımsız ihtiyaç olan objenin referansını (genelde interface veya abstract class) kullandığımız her yerde bu objeyi new'ler.

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
