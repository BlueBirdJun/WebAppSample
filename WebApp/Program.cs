using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Services.Contract;
using WebApp.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
// DB Connection
builder.Services.AddDbContext<DbWebapplication01Context>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLString"));
});

// Custom User Manager
builder.Services.AddScoped<IUserService, UserService>();

// ��Ű ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options =>
    {
        Options.LoginPath = "/Start/Singin";
        Options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// ĳ�� �����(�α׾ƿ� �� ��ȯ ����)
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
            new ResponseCacheAttribute
            {
                NoStore = true,
                Location = ResponseCacheLocation.None,
            }
        );
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");    
    app.UseHsts();
} 
//���� �߿���
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Start}/{action=Singin}/{id?}");

app.Run();
