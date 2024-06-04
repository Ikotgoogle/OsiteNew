 using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using OsiteNew.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyAppContext>(options => options.UseSqlServer(connection));

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
    opiton => {
        opiton.LoginPath = new PathString("/Auth/Login");
        opiton.LogoutPath = new PathString("/Home/HomePage");
    });

/* Свойство LoginPath класса CookieAuthenticationOptions указывает на путь, по которому 
 * неаутентифицированный клиент будет автоматически переадресовываться при обращении к ресурсу, 
 * для доступа к которому требуется аутентификация*/

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();



app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=HomePage}"
);

app.MapControllerRoute(
    name: "userProfile",
    pattern: "Profile/ProfilePage/{userId}",
    defaults: new { controller="Profile", action="ProfilePage" }
);

app.Run();
