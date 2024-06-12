using Assets.API.Middleware;
using Assets.Common.Dtos;
using Assets.Data;
using Common;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRepository<AssetDto>, AssetsRepository>();
builder.Services.Configure<CsvFile>(
    builder.Configuration.GetSection("CsvFile"));
builder.Services.AddSingleton<ICsvDataReader, CsvDataFileReader>();
builder.Services.AddSingleton<IFileReader, FileReader>();

// Add services to the container.

builder.Services.AddControllers();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler(options =>
    {
        _ = options.UseMiddleware<ExceptionHandler>();
    });

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
