using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace IPInfoCommon.Helpers
{
    public static class HttpServerRequest
    {
        public static async Task<string> GetHttpResponse(this string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
