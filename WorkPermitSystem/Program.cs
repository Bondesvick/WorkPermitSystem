using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkPermitSystem.Models.DomainModels;
using WorkPermitSystem.Services;
using WorkPermitContext = WorkPermitSystem.Models.DataLayer.WorkPermitContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddDbContext<WorkPermitContext>(options =>
          options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("WorkPermitContext")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<WorkPermitContext>()
         .AddDefaultTokenProviders();

builder.Services.AddScoped<IFileUploader, S3FileUploader>();
builder.Services.AddScoped<IEmailService, EmailService>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
    name: "aprover",
    areaName: "Aprover",
    pattern: "Aprover/{controller=Permit}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

//WorkPermitContext.CreateAdminUser(app.Services).Wait();

app.Run();