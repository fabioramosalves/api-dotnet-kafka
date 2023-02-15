using WebApp.Business;
using WebApp.Configuration;
using WebApp.Models.Passenger;
using WebApp.Services.Kafka;
using WebApp.Services.Kafka.Consumer;
using WebApp.Services.Kafka.Producer;

var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (string.IsNullOrEmpty(currentEnv))
{
    currentEnv = "Development";
    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", currentEnv);
}

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Load configuration from appsettings.

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));

builder.Services.AddTransient<IMessage, Message>();

if (bool.Parse(builder.Configuration.GetSection("Kafka:active").Value))
{
    builder.Services.AddTransient<IQueueService<Passenger>, KafkaService>();
    builder.Services.AddTransient<IKafkaConsumer, KafkaConsumer>();
}


var app = builder.Build();

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
