namespace Gotrays.Contract.Services;

public interface IStorageService
{
    void Add(string key, string value);

    void Add<T>(string key, T value);

    void Remove(string key);

    void Delete(string key);

    void RemoveAll();

    string Get(string key);

    T Get<T>(string key);
}