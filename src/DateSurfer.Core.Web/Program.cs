using DateSurfer.Core.Application.Interfaces;
using DateSurfer.Core.Application.Services;
using DateSurfer.Core.Domain.Interfaces;
using DateSurfer.Core.Infrastructure.Data;
using DateSurfer.Core.Infrastructure.Repositories;
using DateSurfer.Core.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFeeRuleRepository, FeeRuleRepository>();
builder.Services.AddScoped<IMembershipFeeCalculator, MembershipFeeCalculator>();
builder.Services.AddScoped<ITestUserService, TestUserService>();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(DateSurfer.Core.Application.Features.Membership.Commands.CalculateMembershipFeeCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

// Diese Zeile ist für die Integration Tests (muss NACH app.Run() kommen)
public partial class Program { }