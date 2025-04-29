using FOE.Maintainance.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Presentaion.Attributes;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;
using Core.Entities.ErrorModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers(cofig =>
{
    cofig.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddJsonOptions(opt => {
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.WriteIndented = true;

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
/*
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
*/
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.First().ErrorMessage // Only first error per field
            );

        var customResponse = new ResponseShape<object>(
            StatusCode: 400,
            message: "One or more validation errors occurred.",
            errors: errors,
            data: null
        );

        return new BadRequestObjectResult(customResponse);
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
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
builder.WebHost.UseUrls("http://*:9990","http://0.0.0.0:9990");
//builder.WebHost.UseUrls("http://0.0.0.0:9990");
var app = builder.Build();

app.HandleExceptions();

app.UseSwagger();
app.UseSwaggerUI();
#region Scalar 
//app.UseSwagger(options =>
//{
//   // options.RouteTemplate = "/openapi/{documentName}.json";
//});
//app.MapScalarApiReference(options =>
//    {
//        options.Title = "Future Of Egypt Maintenance System";
//        options.Theme = ScalarTheme.DeepSpace;
//        options.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
//        //options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
//        options.CustomCss = "";
//        options.ShowSidebar = true;
//        options.WithPreferredScheme("Bearer").
//        WithHttpBearerAuthentication(bearer =>
//        {
//            bearer.Token = "your-bearer-token";
//        });
//    });
#endregion
app.UseHttpsRedirection();
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