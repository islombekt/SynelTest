using Employees.Infrastructure.Data;
using Employees.Infrastructure.Extensions;
using Employees.Web.Extensions;
using Employees.Application.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add Infratstructure services 
builder.Services.AddInfraServices(builder.Configuration);

// add Application services 
builder.Services.AddApplicationServices();

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
