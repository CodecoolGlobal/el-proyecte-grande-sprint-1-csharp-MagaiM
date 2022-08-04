using System.Net;
using System.Text.Json.Serialization;
using ElProyecteGrandeSprint1;
using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ElProyecteGrandeSprint1.Auth;
using Newtonsoft.Json;
using ElProyecteGrandeSprint1.Services;
using ElProyecteGrandeSprint1.Helpers;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => {
            //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("https://el-proyecte-grande-kvmgaming-r.herokuapp.com", "https://el-proyecte-grande-kvmgaming-r.herokuapp.com/register").AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ConnectionSqlite")));

builder.Services.AddControllersWithViews().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;

    }); ;

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthentiactionHandler>("BasicAuthentication", null);

//builder.Services.AddControllers().AddJsonOptions(x =>
//    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddTransient<ApiService>();
builder.Services.AddTransient<ServiceHelper>();
builder.Services.AddTransient<ArticleService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<UserServiceHelper>();
builder.Services.AddTransient<EmailSender>();


//if (!builder.Environment.IsDevelopment())
//{
//    builder.Services.AddHttpsRedirection(options => {
//        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
//        options.HttpsPort = 443;
//    });
//}

var app = builder.Build();

//if (app.Environment.IsProduction())
//{
//    var port = Environment.GetEnvironmentVariable("PORT");
//    app.Urls.Add($"https://*:{Environment.GetEnvironmentVariable("PORT")}");
//}
//Configure the HTTP request pipeline.

Console.WriteLine(Environment.GetEnvironmentVariable("PORT"));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT")}");
