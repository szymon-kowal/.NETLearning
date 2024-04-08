using ServiceProvider;
using ServiceInterface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IWeatherService, WeatherProvider>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();