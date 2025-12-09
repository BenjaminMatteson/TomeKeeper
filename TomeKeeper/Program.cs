using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TomeKeeper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<TomeKeeper.Services.IAPIService, TomeKeeper.Services.APIService>();
builder.Services.AddSingleton<TomeKeeper.Services.ITextFormatterService, TomeKeeper.Services.TextFormatterService>();
//builder.Services.AddTransient<TomeKeeper.ViewModels.SpellDetailsViewModel>();

await builder.Build().RunAsync();
