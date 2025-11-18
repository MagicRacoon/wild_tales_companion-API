using LukeDino.Entities;
using LukeDino.Services;
using Microsoft.EntityFrameworkCore;
using LukeDino.Controllers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using LukeDino.Classes;

var builder = WebApplication.CreateBuilder(args);

// LUKE - CHANGE HERE
//FirebaseApp.Create(new AppOptions
//{
    //Credential = GoogleCredential.FromFile("Secrets/firebase-admin.json")
//});

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<LukeDinoContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("Db")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IDinoService, DinoService>();
builder.Services.AddScoped<IUserService, UserService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FirebaseTokenValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWhen(
    context => !context.Request.Path.StartsWithSegments("/swagger") &&
               !context.Request.Path.StartsWithSegments("/openapi"),
    appBuilder =>
    {
        //LUKE - TOGGLE this o disable auth
        //appBuilder.UseMiddleware<FirebaseAuthMiddleware>();
    });

app.MapControllers();
app.Run();
