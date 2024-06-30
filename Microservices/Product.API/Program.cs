var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5001/");

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.Run();
