
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Json
    {
        public static string Serialize<T>(T value) => JsonSerializer.Serialize(value);
        public static T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value);
        public static async Task SerializeAsync<T>(FileStream file, T value) => await JsonSerializer.SerializeAsync(file, value);
        public static async Task<T> DeserealizeAsync<T>(FileStream file) => await JsonSerializer.DeserializeAsync<T>(file);

    }
}
