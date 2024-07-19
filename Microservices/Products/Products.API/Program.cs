using MassTransit;
using Products.API.Consumers;
using Products.DAL.Data;
using Products.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductsDbContext>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GetAllProductsConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"));
        cfg.ReceiveEndpoint("productsQueue", e =>
        {
            e.PrefetchCount = 20;
            e.UseMessageRetry(r => r.Interval(2, 100));

            e.Consumer<GetAllProductsConsumer>(context);
        });
        //cfg.ConfigureJsonSerializer(settings =>
        //{
        //    settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        //    return settings;
        //});
        //cfg.ConfigureJsonDeserializer(configure =>
        //{
        //    configure.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        //    return configure;
        //});
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();