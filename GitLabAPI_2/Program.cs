using Microsoft.OpenApi.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Swagger yapýlandýrmasý
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GitLab API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<GitLabService>();
builder.Services.AddSingleton<CsprojReader>();
builder.Services.AddSingleton<BranchService>();
builder.Services.AddLogging();
builder.Services.AddHttpClient();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Swagger middleware'ýný ekle
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GitLab API V1");
    c.RoutePrefix = string.Empty; // Swagger UI ana sayfada görüntülenecek
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.MapControllers();
app.Run();
