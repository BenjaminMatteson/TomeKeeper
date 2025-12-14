using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TomeKeeper;
using TomeKeeper.Services;
using TomeKeeper.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DragDropService>();

builder.Services.AddTransient<IExpandableScrollViewModel, ExpandableScrollViewModel>();

builder.Services.AddSingleton<IAPIService, APIService>();
builder.Services.AddSingleton<ITextFormatterService, TextFormatterService>();
builder.Services.AddSingleton<ISpellListItemsCacheService, SpellListItemsCacheService>();

builder.Services.AddSingleton<SavedSpellsService>();
builder.Services.AddSingleton<SpellListItemsCacheService>();
builder.Services.AddSingleton<SpellDetailsCacheService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
