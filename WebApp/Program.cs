using Microsoft.Extensions.Configuration;
using WebApp.Configuration;
using WebApp.Models.Passenger;
using WebApp.Services.Kafka;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
if (bool.Parse(builder.Configuration.GetSection("Kafka:active").Value))
{
    builder.Services.AddTransient<IQueueService<Passenger>, KafkaService>();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
