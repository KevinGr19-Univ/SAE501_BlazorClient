using ClientBlazor_v1.Models;
using ClientBlazor_v1.Models.RoomObjects;
using ClientBlazor_v1.Services;
using ClientBlazor_v1.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientBlazor_v1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazorBootstrap();

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API:Url"]!) });
            builder.Services.AddScoped(sp =>
            {
                var options = new JsonSerializerOptions(JsonSerializerOptions.Default);

                options.PropertyNameCaseInsensitive = true;
                options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.Converters.Add(new RoomObjectJSONConverter());

                return options;
            });

            void AddWSService<TEntity>(string entityRoute)
            {
                builder.Services.AddScoped<IService<TEntity>>(sp => new WSService<TEntity>(
                    entityRoute, 
                    sp.GetRequiredService<HttpClient>(), 
                    sp.GetRequiredService<JsonSerializerOptions>(), 
                    sp.GetRequiredService<IConfiguration>()));
            }

            AddWSService<Building>("api/building");
            AddWSService<Room>("api/room");
            AddWSService<RoomType>("api/roomType");
            AddWSService<RoomObject>("api/roomObjects");
            builder.Services.AddScoped<IDTOService, DTOService>();

            await builder.Build().RunAsync();
        }
    }
}
