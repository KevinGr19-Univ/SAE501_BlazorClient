namespace ClientBlazor_v1.Services
{
    public interface IService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> PostAsync(TEntity entity);
        Task PutAsync(int id, TEntity entity);
        Task DeleteAsync(int id);
    }
}
