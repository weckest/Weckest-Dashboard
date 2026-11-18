using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dashboard;
using Weckest.Game;
using Grpc.Net.Client.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddGrpcClient<GameService.GameServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:5098");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new GrpcWebHandler(
        GrpcWebMode.GrpcWeb,
        new HttpClientHandler()
    );
});

await builder.Build().RunAsync();
