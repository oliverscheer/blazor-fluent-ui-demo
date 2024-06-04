using System.Text.Json;

namespace ConcertDatabase.Entities
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonSerializer.Serialize(self);
            var result = JsonSerializer.Deserialize<T>(serialized) ?? default!;
            return result;
        }
    }
}
