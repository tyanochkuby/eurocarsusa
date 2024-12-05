using EuroCarsUSA.Data;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Data.Repositories;
using EuroCarsUSA.Services;
using EuroCarsUSA.Extensions;
using EuroCarsUSA.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using EuroCarsUSA.Data.Attributes;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using EuroCarsUSA.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/Home/Components/Buttons/{0}.cshtml");
    });
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IValidationAttributeAdapterProvider, AtLeastOnePropertyValidationAttributeAdapterProvider>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICustomOrderRepository, CustomOrderRepository>();
builder.Services.AddScoped<ICustomOrderService, CustomOrderService>();
builder.Services.AddScoped<IDetailPageFormRepository, DetailPageFormRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddScoped<ICatalogEditingService, CatalogEditingService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<Localizer>();

// Adding session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
});

// Configure services
var connectionString = builder.Configuration.GetConnectionString("EuroCarsUSA");
builder.Services.Configure<CookieNames>(
            builder.Configuration.GetSection("CookieNames")
        );
builder.Services.Configure<EmailSettings>(
               builder.Configuration.GetSection("EmailSettings")
        );
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, options => options.CommandTimeout(90));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationExpanders.Add(new CustomViewLocationExpander());
});


var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    Seed.SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var supportedCultures = new[] { new CultureInfo("pl-PL"), new CultureInfo("en") };
var requestLocalizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pl-PL"), 
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new IRequestCultureProvider[]
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    }
};

app.UseRequestLocalization(requestLocalizationOptions);

app.UseRouting();

app.UseAuthorization();
app.MapBlazorHub();

app.UseSession(); // Use session middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();