using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TomeKeeper;
using TomeKeeper.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IAPIService, APIService>();
builder.Services.AddSingleton<ITextFormatterService, TextFormatterService>();
builder.Services.AddSingleton<ISpellListItemsCacheService, SpellListItemsCacheService> ();
builder.Services.AddScoped<DragDropService>();
builder.Services.AddSingleton<PinnedSpellsService>();
builder.Services.AddSingleton<SpellListItemsCacheService>();
builder.Services.AddSingleton<SpellDetailsCacheService>();

await builder.Build().RunAsync();
