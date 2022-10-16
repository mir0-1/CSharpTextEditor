using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Drawing;

namespace CSharpTextEditor
{
    class ImageParser
    {
        private static bool bOnce = false;
        private static HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseProxy = false, Proxy = null, MaxResponseHeadersLength = 10000 });
        private HttpResponseMessage lastResponse;

        private StringBuilder output = new StringBuilder();
        private int widthInternal;
        private int heightInternal;

        public int width
        {
            get => widthInternal;
        }
        public int height
        {
            get => heightInternal;
        }

        public string outputString
        {
            get => output.ToString();
        }

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

        public bool FetchImageAsBase64(string url, int timeout)
        {
            byte[] resultBytes;
            string mediaType;

            output.Clear();

            if (!IsLocalPath(url))
            {
                Task<byte[]> t = Task.Run(() => DownloadImageInternal(url));
                t.Wait(timeout);

                if (!t.IsCompleted)
                    return false;

                resultBytes = t.Result;
                mediaType = lastResponse.Content.Headers.ContentType.MediaType;
            }
            else
            {
                try
                {
                    resultBytes = File.ReadAllBytes(url);
                    mediaType = "image/" + System.IO.Path.GetExtension(url).Replace(".", "");
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            if (resultBytes.Length == 0)
                return false;

            output.Append("<img src=\"data:");
            output.Append(mediaType);
            output.Append(";base64,");
            output.Append(Convert.ToBase64String(resultBytes));
            output.Append("\">");

            try
            {
                using (MemoryStream ImageStream = new System.IO.MemoryStream(resultBytes))
                {
                    Image loadedImage = Image.FromStream(ImageStream);
                    widthInternal = loadedImage.Width;
                    heightInternal = loadedImage.Height;
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
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
