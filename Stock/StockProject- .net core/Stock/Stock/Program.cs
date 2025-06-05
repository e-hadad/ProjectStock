using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// הגדרת מדיניות CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });

    // מדיניות כללית (אם תרצי לאפשר גישה מכל מקום)
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// שירותים נוספים
builder.Services.AddControllers();
builder.Services.AddScoped<AlphaVantageService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// תצורת פיתוח
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// הפעלת CORS – בחרי איזו מדיניות להשתמש
app.UseCors(MyAllowSpecificOrigins); // או: "AllowAllOrigins"

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();













