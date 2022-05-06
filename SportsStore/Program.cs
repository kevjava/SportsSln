using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(
    opts =>
    {
        // opts.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
        // opts.UseSqlite();
        opts.UseSqlite("Data Source=SportsStore.db");
    }
);

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.MapControllerRoute("pagination", "/Products/Page{productPage}", new { Controller="Home", action="Index"});
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
