using System.Text;
using System.Text.Json;

namespace Yam.Core.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T?> ReadAsAsync<T>(this HttpContent content, JsonSerializerOptions options = null)
        {
            if (content.Headers.ContentLength == 0)
            {
                return default(T);
            }

            using (Stream contentStream = await content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(contentStream, options ?? defaultOptions);
            }
        }

        public static StringContent ToHttpStringContent(this object obj)
        {
            _ = obj ?? throw new ArgumentNullException(nameof(obj));

            return new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        private static readonly JsonSerializerOptions defaultOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null,
        };
    }
}
