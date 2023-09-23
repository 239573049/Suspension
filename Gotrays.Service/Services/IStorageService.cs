namespace Gotrays.Contract.Services;

public interface IStorageService
{
    void Add<T>(string name, T value);

    void Delete(string name);

    string? GetString(string name);

    int? GetInt(string name);

    T? Get<T>(string name);

    bool? GetBool(string name);

    double? GetDouble(string name);

    void Clear();
}