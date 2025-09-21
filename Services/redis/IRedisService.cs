public interface IRedisService
{
    Task SetStringAsync(string key, string value);
    Task<string?> getStringAsync(string key);
    Task<string?> GetStringAsync(string v);
}