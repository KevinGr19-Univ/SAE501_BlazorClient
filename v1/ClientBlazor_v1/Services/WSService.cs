using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientBlazor_v1.Services
{
    public abstract class WSService
    {
        protected readonly HttpClient httpClient;
        protected readonly JsonSerializerOptions jsonSettings;

        public WSService(HttpClient client, JsonSerializerOptions jsonSettings, IConfiguration config)
        {
            httpClient = client;
            this.jsonSettings = jsonSettings;

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("API_KEY", config["API:ApiKey"]);
        }

        protected async Task<List<TEntity>> _GetAllAsync<TEntity>(string route)
        {
            return await httpClient.GetFromJsonAsync<List<TEntity>>(route, options: jsonSettings);
        }

        protected async Task<TEntity?> _GetByIdAsync<TEntity>(string prefix_route, int id)
        {
            return await httpClient.GetFromJsonAsync<TEntity>($"{prefix_route}/{id}", options: jsonSettings);
        }

        protected async Task<TEntity> _PostAsync<TEntity>(string route, TEntity entity)
        {
            var res = await httpClient.PostAsJsonAsync(route, entity, options: jsonSettings);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<TEntity>();
        }

        protected async Task _PutAsync<TEntity>(string prefix_route, int id, TEntity entity)
        {
            var res = await httpClient.PutAsJsonAsync($"{prefix_route}/{id}", entity, options: jsonSettings);
            res.EnsureSuccessStatusCode();
        }

        protected async Task _DeleteAsync(string prefix_route, int id)
        {
            var res = await httpClient.DeleteAsync($"{prefix_route}/{id}");
            res.EnsureSuccessStatusCode();
        }
    }

    public class WSService<TEntity> : WSService, IService<TEntity>
    {
        private readonly string _entityRoute;

        public WSService(string entityRoute, HttpClient client, JsonSerializerOptions jsonSettings, IConfiguration config) : base(client, jsonSettings, config)
        {
            _entityRoute = entityRoute;
        }

        public virtual Task<List<TEntity>> GetAllAsync() => _GetAllAsync<TEntity>(_entityRoute);

        public virtual Task<TEntity?> GetByIdAsync(int id) => _GetByIdAsync<TEntity>($"{_entityRoute}/GetById", id);

        public virtual Task<TEntity> PostAsync(TEntity entity) => _PostAsync(_entityRoute, entity);

        public virtual Task PutAsync(int id, TEntity entity) => _PutAsync(_entityRoute, id, entity);

        public virtual Task DeleteAsync(int id) => _DeleteAsync(_entityRoute, id);
    }
}
