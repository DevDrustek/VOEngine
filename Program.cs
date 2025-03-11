using Microsoft.EntityFrameworkCore;
using VBEngine;
using VBEngine.Models;
using VBEngine.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = 
    System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();
builder.Services.AddAntiforgery();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<DrustEngineContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));
//builder.Services.AddDbContext<DrustEngineContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetSection("ConnectionString")["ConnectionString"]));
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<ProviderServices>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



var app = builder.Build();
//app.UseMiddleware<ApiKeyMiddleware>();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
