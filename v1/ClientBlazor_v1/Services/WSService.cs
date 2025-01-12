using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientBlazor_v1.Services
{
    public class WSService<TEntity> : IService<TEntity>
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSettings;
        private readonly string _entityRoute;

        public WSService(string entityRoute, HttpClient client, JsonSerializerOptions jsonSettings)
        {
            _entityRoute = entityRoute;
            _httpClient = client;
            _jsonSettings = jsonSettings;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TEntity>>(_entityRoute, options: _jsonSettings);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TEntity>($"{_entityRoute}/GetById/{id}", options: _jsonSettings);
        }

        public virtual async Task<TEntity> PostAsync(TEntity entity)
        {
            var res = await _httpClient.PostAsJsonAsync(_entityRoute, entity, options: _jsonSettings);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<TEntity>();
        }

        public virtual async Task PutAsync(int id, TEntity entity)
        {
            var res = await _httpClient.PutAsJsonAsync($"{_entityRoute}/{id}", entity, options: _jsonSettings);
            res.EnsureSuccessStatusCode();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var res = await _httpClient.DeleteAsync($"{_entityRoute}/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
