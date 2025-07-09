using Application.Contracts;
using Application.Models;
using Confluent.Kafka;
using Services.Installers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Workers.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();


// Sample of a Cancellation Token for the application.
CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true; // prevent the process from terminating.
    cts.Cancel();
};

// Add services to the container.
var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "equipment-info-update",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var serializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNameCaseInsensitive = true,

};

const string topic = "test-equipment";

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
    consumer.Subscribe(topic);
    try
    {
        while (true)
        {
            var consumeResult = consumer.Consume(cts.Token);
            Console.WriteLine($"Consumed event from topic {topic}: key = {consumeResult.Message.Key,-10} value = {consumeResult.Message.Value}");
            try
            {
                var equipmentInfo = JsonSerializer.Deserialize<EquipmentInfoUpdateEvent>(
                    consumeResult.Message.Value,
                    serializerOptions);


                if (equipmentInfo!.EventType == EventType.EquipmentStatusUpdate)
                {
                    Console.WriteLine($"Received EquipmentStatusUpdate for equipment {equipmentInfo.EquipmentId} with status {equipmentInfo.Status}");
                    UpdateEquipmentStatus(equipmentInfo);
                }
                else if (equipmentInfo.EventType == EventType.OrderUpdate)
                {
                    Console.WriteLine($"Received OrderUpdate for equipment {equipmentInfo.EquipmentId} with orders count {equipmentInfo.CurrentOrders!.Count()}");
                    UpdateEquipmentOrders(equipmentInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception parsing event. Discarting message. Exception: {ex}");
                continue;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception thrown in consumer. Closing process. Exception: {ex}");
    }
    finally
    {
        consumer.Close();
    }
}

void UpdateEquipmentOrders(EquipmentInfoUpdateEvent equipmentInfo)
{
    var currentTime = DateTime.UtcNow;

    var equipmentEntity = new Equipment
    {
        Id = equipmentInfo!.EquipmentId,
        Status = OperationStatus.Undefined,
        Sector = string.Empty,
        CurrentOrders = equipmentInfo.CurrentOrders!.Select(o => new Order
        {
            Id = o.OrderId,
            Description = o.Description,
            Status = o.Status,
            CreatedAt = currentTime
        }),
        UpdatedAt = currentTime,
        CreatedAt = currentTime
    };
    app.Services
        .GetRequiredService<IEquipmentRepository>()
        .ReplaceOrdersAsync(equipmentEntity).GetAwaiter().GetResult();
}

void UpdateEquipmentStatus(EquipmentInfoUpdateEvent equipmentInfo)
{
    var currentTime = DateTime.UtcNow;

    var equipmentEntity = new Equipment
    {
        Id = equipmentInfo!.EquipmentId,
        Status = (OperationStatus)equipmentInfo.Status!,
        Sector = equipmentInfo.Sector!,
        CurrentOrders = [],
        UpdatedAt = currentTime,
        CreatedAt = currentTime
    };
    app.Services
        .GetRequiredService<IEquipmentRepository>()
        .UpsertEquipmentAsync(equipmentEntity).GetAwaiter().GetResult();
}

app.Run();
