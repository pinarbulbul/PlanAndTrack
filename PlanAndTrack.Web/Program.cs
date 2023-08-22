using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanAndTrack.Application.Mapper;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Application.Services;
using PlanAndTrack.Application.Services.Interfaces;
using PlanAndTrack.Application.Validations;
using PlanAndTrack.Infrastructure.EntityFrameworkCore.ApplicationIdentity;
using PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<TestRequestDbContext>(options =>
    options.UseNpgsql(connectionString)
);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ITestRequestService, TestRequestService>();
builder.Services.AddScoped<IPlanService, PlanService>();

builder.Services.AddScoped<IValidations, Validations>();

builder.Services.AddScoped<ITestRequestRepository, TestRequestRepository>();
builder.Services.AddScoped<IPlanTestRequestRepository, PlanTestRequestRepository>();
builder.Services.AddScoped<IPlanPeriodRepository, PlanPeriodRepository>();
builder.Services.AddScoped<IPlanPeriodResourceRepository, PlanPeriodResourceRepository>();
builder.Services.AddScoped<IPlanPerformanceRepository, PlanPerformanceRepository>();


builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();

