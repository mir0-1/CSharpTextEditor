using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace CSharpTextEditor
{
    class ImageParser
    {
        private static bool bOnce = false;
        private static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseProxy = false, Proxy = null, MaxResponseHeadersLength = 10000 });
        private HttpResponseMessage lastResponse;

        public ImageParser()
        {
            if (!bOnce)
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept", "image/jpeg, image/png, image/bmp");
                bOnce = true;
            }
        }
        private static bool IsLocalPath(string p)
        {
            return new Uri(p).IsFile;
        }

        public string FetchImageAsBase64(string url, int timeout)
        {
            byte[] result;
            string mediaType;

            if (!IsLocalPath(url))
            {
                Task<byte[]> t = Task.Run(() => DownloadImageInternal(url));
                t.Wait(timeout);

                if (!t.IsCompleted)
                    return null;

                result = t.Result;
                mediaType = lastResponse.Content.Headers.ContentType.MediaType;
            }
            else
            {
                try
                {
                    result = File.ReadAllBytes(url);
                    mediaType = "image/" + System.IO.Path.GetExtension(url).Replace(".", "");
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return "<img src=\"data:" +
                    mediaType +
                    ";base64," +
                    Convert.ToBase64String(result) + "\">&#8203;";
        }

        private async Task<byte[]> DownloadImageInternal(string url)
        {
            byte[] result;
            try
            {
                lastResponse = await httpClient.GetAsync(url);
                result = await lastResponse.Content.ReadAsByteArrayAsync();
            }
            catch (Exception e)
            {
                return null;
            }

            return result;
        }
    }
}
