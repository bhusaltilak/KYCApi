using KycApi.Data;
using KycApi.Repositories;
using KycApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC",
        policy => policy.WithOrigins("https://localhost:5189") // ✅ Ensure this matches MVC port
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<DbHelper>();

builder.Services.AddScoped<IKycRepository, KycRepository>();
builder.Services.AddScoped<IKycService, KycService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowMVC");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
