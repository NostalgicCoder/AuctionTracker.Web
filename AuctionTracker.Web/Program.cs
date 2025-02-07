using AuctionTracker.Web.Class;
using AuctionTracker.Web.Data;
using AuctionTracker.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Register with dependancy injection container
builder.Services.AddSingleton<IValidation, Validation>();
builder.Services.AddSingleton<ISortData, SortData>();
builder.Services.AddSingleton<IPopulateProductModel, PopulateProductModel>();
builder.Services.AddSingleton<IProcessIgdb, ProcessIgdb>();
builder.Services.AddSingleton<IGeneralHelper, GeneralHelper>();
builder.Services.AddSingleton<IPopulateControls, PopulateControls>();
builder.Services.AddSingleton<ICalculateTrends, CalculateTrends>();
builder.Services.AddSingleton<ICalculatePrices, CalculatePrices>();

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
