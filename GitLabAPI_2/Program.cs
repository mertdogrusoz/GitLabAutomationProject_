var builder = WebApplication.CreateBuilder(args);

// CORS politikas�n� tan�mla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // T�m kaynaklardan eri�ime izin ver
                   .AllowAnyMethod() // T�m HTTP y�ntemlerine izin ver
                   .AllowAnyHeader(); // T�m ba�l�klara izin ver
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
