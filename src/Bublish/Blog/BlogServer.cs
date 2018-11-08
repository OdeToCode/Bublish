using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Bublish.Blog
{
    // TODO: Get rid of Task.Result

    public class BlogServer : IBlogServer
    {
        private readonly ILogger<BlogServer> logger;
        private readonly HttpClient httpClient;

        public BlogServer(ILogger<BlogServer> logger)
        {
            var handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true
            };

            httpClient = new HttpClient(handler);
            this.logger = logger;
        }

        public int AddPost()
        {
            throw new NotImplementedException();
        }

        public void Initialize(string server, string username, string password)
        {
            var inputs = new Dictionary<string, string>()
            {
                { "username", username },
                { "password", password }
            };

            var request = new FormUrlEncodedContent(inputs);

            httpClient.BaseAddress = new Uri(server);
            var response = httpClient.PostAsync("/admin/login", request).Result;

            response.EnsureSuccessStatusCode();
            logger.LogInformation("BlogServer logged in and initialized");
        }

        public string UploadMedia(string localName, byte[] bits)
        {
            var media = new MediaObject()
            {
                bits = bits,
                name = localName,
                type = Path.GetExtension(localName)
            };

            throw new NotImplementedException();
        }
    }
}
