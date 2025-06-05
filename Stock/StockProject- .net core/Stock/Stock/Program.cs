using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// ����� ������� CORS
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

    // ������� ����� (�� ���� ����� ���� ��� ����)
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ������� ������
builder.Services.AddControllers();
builder.Services.AddScoped<AlphaVantageService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ����� �����
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ����� CORS � ���� ���� ������� ������
app.UseCors(MyAllowSpecificOrigins); // ��: "AllowAllOrigins"

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();













