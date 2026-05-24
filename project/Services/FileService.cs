using System.Text.Json;
using TaskHub.Models;

namespace TaskHub.Services;

public class FileService : IDisposable
{
    private bool _disposed;

    public async Task SaveAsync(List<TaskItem> tasks, string path)
    {
        try
        {
            string directory = Path.GetDirectoryName(path) ?? ".";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(tasks, options);
            await File.WriteAllTextAsync(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save error: {ex.Message}");
        }
    }

    public async Task<List<TaskItem>> LoadAsync(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                return new List<TaskItem>();
            }

            string json = await File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<List<TaskItem>>(json)
                   ?? new List<TaskItem>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load error: {ex.Message}");
            return new List<TaskItem>();
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }
}
