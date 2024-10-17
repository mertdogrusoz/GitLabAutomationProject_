var builder = WebApplication.CreateBuilder(args);

// CORS politikasýný tanýmla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // Tüm kaynaklardan eriþime izin ver
                   .AllowAnyMethod() // Tüm HTTP yöntemlerine izin ver
                   .AllowAnyHeader(); // Tüm baþlýklara izin ver
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<GitLabService>(); // Servisleri burada ekle
builder.Services.AddSingleton<CsprojReader>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// CORS'u kullan
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
