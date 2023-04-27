using NLog;

using BambooChronoSyncUtility.Service.Services;
using NLog.Web;
using System.Net.Http.Headers;
using System.Text;
using BambooChronoSyncUtility.DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TimeTrackDbConnection") ??
                       throw new Exception("Missed config value 'ConnectionStrings:TimeTrackDbConnection'");

builder.Services.AddDbContext<ChronoContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers().AddNewtonsoftJson();
// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddHttpClient("BambooHR_API", c =>
{
    c.BaseAddress = new Uri("https://api.bamboohr.com/api/gateway.php/altoros/v1/");
    //c.DefaultRequestHeaders.Add("Authorization", "Basic ODc0YjZlYTAzOWEwYTY4NWU2MGQzYWIzMjY3ZjU0NzMwY2JhNDdlZjo=");
    var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"90ba85c8e7a1b5ea5d0304e6277286e26acd299e:x"));
    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

    c.DefaultRequestHeaders.Add("Accept", "application/json");
});
//.AddTransientHttpErrorPolicy(
//            x => x.WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(Math.Pow(10, retryAttempt))));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBambooHrAPIService, BambooHrAPIService>();
builder.Services.AddScoped<IBambooHrService, BambooHrService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
