using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SetareSazBot.API.Json.Output;
using SetareSazBot.DAL;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class ApiService : IApiService
    {
        private readonly ApplicationDbContext _context;

        public ApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null) where T : class
        {

            var apiResult = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }
            }

            using (var response = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                apiResult = await reader.ReadToEndAsync();
            }

            return string.IsNullOrEmpty(apiResult) ? null : JsonConvert.DeserializeObject<T>(apiResult);
        }

        public async Task<T> PostAsync<T>(string url, object body = null, Dictionary<string, string> headers = null) where T : class
        {

            var apiResult = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(body,
                    Newtonsoft.Json.Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                //var json = JsonConvert.SerializeObject(body);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }


            using (var response = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                apiResult = await reader.ReadToEndAsync();
            }

            return string.IsNullOrEmpty(apiResult) ? null : JsonConvert.DeserializeObject<T>(apiResult);
        }

        public async Task<T> PutAsync<T>(string url, object body = null, Dictionary<string, string> headers = null) where T : class
        {

            var apiResult = "";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                if (url.Contains("bime"))
                {
                    var json = JsonConvert.SerializeObject(body);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(body,
                        Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }


            using (var response = (HttpWebResponse)await httpWebRequest.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                apiResult = await reader.ReadToEndAsync();
            }

            return string.IsNullOrEmpty(apiResult) ? null : JsonConvert.DeserializeObject<T>(apiResult);
        }

        public async Task<bool> Download(string url, string fileName)
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

        public async Task<UploadOutput> Upload(string filePath, string url, Dictionary<string, string> headers)
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