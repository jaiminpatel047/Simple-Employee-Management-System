using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleCurd.DataAccess;
using SimpleCurd.DataAccess.Interface;
using SimpleCurd.DataAccess.Repository;
using SimpleCurd.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// add DB context
builder.Services.AddDbContext<ApplicationDbContext>(Options => 
Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register generic repository
builder.Services.AddScoped(typeof(IRepostiory<>), typeof(Repostiory<>));



// Add Repository DI
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();


// Add Services DI
builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();


// Add services to the container.
builder.Services.AddControllersWithViews();

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
