using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using PSR_Add_Document.Models;
using PSR_Add_Document.Models.GlobalClass;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var conString = builder.Configuration.GetConnectionString("Con");

builder.Services.AddDbContext<CustomerDbContext>(x => x.UseSqlServer(conString));


builder.Services.AddHttpContextAccessor();//New code add for session
builder.Services.AddSession();//New code add for session


//---
var configuration = builder.Configuration;

builder.Services.Configure<Config>(configuration.GetSection("Config"));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<Config>();
//---

builder.Services.AddAuthentication("MyAuthScheme")
    .AddCookie("MyAuthScheme", options =>
    {
        options.LoginPath = "/Home/Index"; // Specify the login page URL
        options.LogoutPath = "/Home/Logout"; // Specify the logout page URL
    });



// Configure the HTTP request pipeline



builder.Services.AddControllersWithViews();//login

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.UseSession();//New code add for session

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseAuthentication();//=Authentication
app.UseAuthorization();
app.UseRouting();

//enable session before MVC A
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
