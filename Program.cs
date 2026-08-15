using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserApi.Blazor;
using UserApi.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5079") });

// DI Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskItemService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<LocalStorageService>();

await builder.Build().RunAsync();
