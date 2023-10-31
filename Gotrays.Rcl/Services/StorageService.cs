using Gotrays.Contract.Services;
using LiteDB;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Gotrays.Rcl.Services;

public class StorageService : IStorageService, IDisposable
{
    private readonly LiteDatabase _liteDatabase;

    private readonly ILiteCollection<Storage> _objects;

    public StorageService()
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GotraysAI");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        _liteDatabase = new LiteDatabase(Path.Combine(path, "storage.db"));

        _objects = _liteDatabase.GetCollection<Storage>(nameof(StorageService));
    }

    public void Add<T>(string name, T value)
    {
        if (_objects.Exists(x => x.Name == name))
            Delete(name);

        if (value is string str)
        {
            _objects.Insert(new Storage(name, str));
        }
        else if (value is int or bool or double)
        {
            _objects.Insert(new Storage(name, value?.ToString()));
        }
        else
        {
            _objects.Insert(new Storage(name, JsonSerializer.Serialize(value)));
        }
    }

    public void Delete(string name)
    {
        _objects.DeleteMany(x => x.Name == name);
    }

    public string? GetString(string name)
    {
        var result = _objects.Find(x => x.Name == name).FirstOrDefault();

        return result?.Value;
    }

    public int? GetInt(string name)
    {
        var result = _objects.Find(x => x.Name == name).FirstOrDefault();

        if (string.IsNullOrEmpty(result?.Value))
        {
            return null;
        }

        return Convert.ToInt16(result.Value);
    }

    public T? Get<T>(string name)
    {
        var result = _objects.Find(x => x.Name == name).FirstOrDefault();
        return string.IsNullOrEmpty(result?.Value) ? default : JsonSerializer.Deserialize<T>(result.Value);
    }

    public bool? GetBool(string name)
    {
        var result = _objects.Find(x => x.Name == name).FirstOrDefault();

        if (string.IsNullOrEmpty(result?.Value))
        {
            return null;
        }

        return Convert.ToBoolean(result.Value);
    }

    public double? GetDouble(string name)
    {
        var result = _objects.Find(x => x.Name == name).FirstOrDefault();

        if (string.IsNullOrEmpty(result?.Value))
        {
            return null;
        }

        return Convert.ToDouble(result.Value);
    }

    public void Clear()
    {
        _objects.DeleteAll();
    }

    public void Dispose()
    {
        _liteDatabase.Dispose();
    }

    public class Storage
    {
        public Storage(string name, string? value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public string? Value { get; set; }
    }
}