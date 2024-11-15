
using DataEncodingApi.Controllers;
using DataEncodingApi.Interfaces.IServices;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the SOAP service
builder.Services.AddSingleton<IEncodingService, DataEncodingController>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}else
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
    app.Urls.Add($"http://*:{port}");
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IEncodingService>(
        "/soap/encoding.asmx",
        new SoapEncoderOptions(),
        SoapSerializer.XmlSerializer
    );
    endpoints.MapControllers();
});

app.Run();
