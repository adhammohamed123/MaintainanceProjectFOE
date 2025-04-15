
using FOE.Maintainance.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog;
using Presentaion.Attributes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers(cofig =>
{
    cofig.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddJsonOptions(opt => {
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
})
    .AddApplicationPart(typeof(Presentaion.AssemblyReference).Assembly);

builder.Services.AddAuthentication();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.ConfigureCORS()
.AddAutoMapper(typeof(Program))
.ConfigureLogger()
.ConfigureDbContext(builder.Configuration)
.ConfigureRepositoryManager()
.ConfigureServiceManager()
.ConfigureIdentity()
.ConfigureJWT(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Place to add JWT with Bearer",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
      s.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
        {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
        {
        Type = ReferenceType.SecurityScheme,
        Id = "Bearer"
      
        },
        Name = "Bearer",
        },
        new List<string>()
        }
        });
});
/*builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7193;
});*/
builder.WebHost.UseUrls("http://*:9990");

var app = builder.Build();

app.HandleExceptions();


app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
.Services.BuildServiceProvider()
.GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
.OfType<NewtonsoftJsonPatchInputFormatter>().First();