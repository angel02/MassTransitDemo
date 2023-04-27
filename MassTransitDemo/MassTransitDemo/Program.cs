using MassTransit;
using MassTransitDemo.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        var uri = new Uri(builder.Configuration["RabbitMQ:Endpoint"]);

        config.Host(uri, "TestConecction", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:User"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapPost("api/product/create", async (Product product, IPublishEndpoint publishEndpoint) =>
{
    if (product != null)
    {
        await publishEndpoint.Publish<Product>(product);

        return "Ok";
    }

    return "badrequest";
});

app.Run();