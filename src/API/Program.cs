using Dotby.Extensions;
using Dotby.Domain;
using Serilog;
using Dotby.Application.Mappings;
using Dotby.API.ActionFilters;
using DotNetEnv;


Env.Load();


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Logging.ClearProviders();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Host.ConfigureSerilogService(); 
builder.Services.ConfigureLoggerService();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigurePostGressContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddControllers();
builder.Services.AddCloudinaryConfiguration(builder.Configuration);
builder.Services.ConfigurePhotoService();
builder.Services.ConfigureRepositoryManager();  
builder.Services.AddSignalR();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>(); 
app.ConfigureExceptionHandler(logger); 

if (app.Environment.IsProduction())
{
    app.UseHsts();
}


app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotby v1");
});


app.UseHttpsRedirection();


app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

