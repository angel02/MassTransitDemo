using MassTransit;
using MassTransitDemo.Consumer.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    //x.AddRequestClient<ProductConsumer>();

    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri(builder.Configuration["RabbitMQ:Endpoint"]), host =>
        {
            host.Username(builder.Configuration["RabbitMQ:User"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        config.ReceiveEndpoint("product", configEndpoint =>
        {
            configEndpoint.Consumer<ProductConsumer>();
            //configEndpoint.PrefetchCount = 16;
            //configEndpoint.UseMessageRetry(r => r.Interval(2, 100));
            //configEndpoint.ConfigureConsumer<ProductConsumer>(provider);
        });
    }));
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () =>
{
    return "ok";
});

app.Run();

