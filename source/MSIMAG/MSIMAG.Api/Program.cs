
using Locwise.Fireberry.Client;
using MSIMAG.Api;
using MSIMAG.Core;
using MSIMAG.Core.Data;
using Microsoft.Extensions.Caching.Memory;

FireberryClientFactory factory=new FireberryClientFactory();
var client = factory.Create(Consts.API_KEY);
IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions()
{
    ExpirationScanFrequency = TimeSpan.FromMinutes(60)
});

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFireberryClient>(client);
builder.Services.AddTransient<IDataManager, DataManager>();
builder.Services.AddTransient<IMappingStore, MappingsDataStore>();
//builder.Services.AddTransient<IMappingStore, MockMappingStore>();
builder.Services.AddSingleton<IMemoryCache>((s) => { return memoryCache; });

// Add services to the container.


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
