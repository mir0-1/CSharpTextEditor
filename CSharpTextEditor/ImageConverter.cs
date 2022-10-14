using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace CSharpTextEditor
{
    class ImageConverter
    {
        private static bool bOnce = false;
        private static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseProxy = false, Proxy = null, MaxResponseHeadersLength = 10000 });
        private HttpResponseMessage lastResponse;

        public ImageConverter()
        {
            if (!bOnce)
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept", "image/jpeg, image/png, image/bmp");
                bOnce = true;
            }
        }

        public string DownloadImageBase64(string url, int timeout)
        {
            Task<byte[]> t = Task.Run(() => DownloadImageInternal(url));
            t.Wait(timeout);

            return "data:" + 
                    lastResponse.Content.Headers.ContentType.MediaType + 
                    ";base64," +
                    Convert.ToBase64String(t.Result);
        }

        private async Task<byte[]> DownloadImageInternal(string url)
        {
            lastResponse = await httpClient.GetAsync(url);
            byte[] result = await lastResponse.Content.ReadAsByteArrayAsync();

            return result;
        }
    }
}
