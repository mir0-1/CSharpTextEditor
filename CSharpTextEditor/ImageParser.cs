using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Drawing;

/*
 * This application uses iText. iText Copyright notice:
 * 
 Copyright (c) 1998-2022 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.
This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS
This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/
The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.
In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.
You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.
For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
 */

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
            bool result = false;
            try
            {
                result = new Uri(p).IsFile;
            }
            catch (Exception e)
            {
                return false;
            }

            return result;
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

                if (!t.IsCompleted || t.Result == null)
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
