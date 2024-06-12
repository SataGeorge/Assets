using Common;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<EndPoint>(
    builder.Configuration.GetSection("AssetsEndPoint"));
builder.Services.AddHttpClient(HttpServiceGateway.HttpClientName);
builder.Services.AddSingleton<IServiceGateway, HttpServiceGateway>();


// Add services to the container.
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
