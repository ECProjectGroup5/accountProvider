using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

//Registrering av IDentity
builder.Services.AddIdentity<UserEntity, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<DataContext>();

//Configurera Cookies
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin"; //g�r till anvigen address
    x.Cookie.HttpOnly = true; //bara kan l�sas ut av servern
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;  //alltid ska anv sig av Https
    x.ExpireTimeSpan = TimeSpan.FromHours(1); //sessionen �r giltig i 1 h fram�t
    x.SlidingExpiration = true; //vid aktivitet f�rl�ngs "expiretime"
});




var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();