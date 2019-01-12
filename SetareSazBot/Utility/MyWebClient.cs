using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SetareSazBot.API.Json.Output;

namespace SetareSazBot.Utility
{
    public static class MyWebClient
    {
        public static async Task<bool> Download(string url, string fileName)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var folder = Path.GetDirectoryName(fileName);
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    await client.DownloadFileTaskAsync(url, fileName);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<UploadOutput> UploadImage(string filePath, string url, Dictionary<string, string> headers)
        {
            try
            {
                using (var client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    var fileNameOnly = Path.GetFileName(filePath);
                    var fileContent = new StreamContent(File.OpenRead(filePath));
                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = fileNameOnly,

                    };

                    content.Add(fileContent, "file", "myFileName.jpg");

                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    var statusResult = await client.PostAsync(url, content);
                    var statusString = await statusResult.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UploadOutput>(statusString);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}