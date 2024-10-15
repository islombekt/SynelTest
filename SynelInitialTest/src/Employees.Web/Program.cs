using Employees.Application.Services;
using Employees.Application.Services.Handlers.Error;
using Employees.Infrastructure.Data;
using Employees.Infrastructure.Extensions;
using Employees.Web.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add Infratstructure services 
builder.Services.AddInfraServices(builder.Configuration);

// add services 

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeValidationService, EmployeeValidationService>();
builder.Services.AddScoped<IErrorHandler, ErrorHandler>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();
// Apply migration
app.MigrateDatabase<ApplicationDbContext>();
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
