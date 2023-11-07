using System.Text.Json;
using Gotrays.Contract.Services;

namespace Gotrays.Application.Services;

public class StorageService : IStorageService
{
    private string filePath = "Gotrays.json";
    private Dictionary<string, string> cache = new Dictionary<string, string>();

    public void Add(string key, string value)
    {
        Dictionary<string, string> data = LoadData();
        data[key] = value;
        SaveData(data);
        UpdateCache(key, value);
    }

    public void Add<T>(string key, T value)
    {
        Dictionary<string, T> data = LoadData<T>();
        data[key] = value;
        SaveData(data);
        UpdateCache(key, value.ToString());
    }

    public void Remove(string key)
    {
        Dictionary<string, string> data = LoadData();
        data.Remove(key);
        SaveData(data);
        RemoveFromCache(key);
    }

    public void Delete(string key)
    {
        Remove(key);
    }

    public void RemoveAll()
    {
        File.Delete(filePath);
        ClearCache();
    }

    public string Get(string key)
    {
        if (cache.ContainsKey(key))
        {
            return cache[key];
        }
        else
        {
            Dictionary<string, string> data = LoadData();
            if (data.ContainsKey(key))
            {
                string value = data[key];
                UpdateCache(key, value);
                return value;
            }
            return null;
        }
    }

    public T Get<T>(string key)
    {
        if (cache.ContainsKey(key))
        {
            string value = cache[key];
            return (T)Convert.ChangeType(value, typeof(T));
        }
        else
        {
            Dictionary<string, T> data = LoadData<T>();
            if (data.ContainsKey(key))
            {
                T value = data[key];
                UpdateCache(key, value.ToString());
                return value;
            }
            return default(T);
        }
    }

    private Dictionary<string, string> LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        return new Dictionary<string, string>();
    }

    private Dictionary<string, T> LoadData<T>()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, T>>(json);
        }
        return new Dictionary<string, T>();
    }

    private void SaveData(Dictionary<string, string> data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
    }

    private void SaveData<T>(Dictionary<string, T> data)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
    }

    private void UpdateCache(string key, string value)
    {
        if (cache.ContainsKey(key))
        {
            cache[key] = value;
        }
        else
        {
            cache.Add(key, value);
        }
    }

    private void RemoveFromCache(string key)
    {
        if (cache.ContainsKey(key))
        {
            cache.Remove(key);
        }
    }

    private void ClearCache()
    {
        cache.Clear();
    }
}