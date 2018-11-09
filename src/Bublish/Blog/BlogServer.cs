using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        public BlogPostResponse AddPost(DateTime publishDate, string htmlContent, string title, string slug)
        {
            var post = new Post()
            {
                dateCreated = publishDate,
                description = htmlContent,
                title = title,
                wp_slug = slug
            };

            var input = JsonConvert.SerializeObject(post);
            var request = new StringContent(input, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("/api/blogpost", request).Result;

            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<BlogPostResponse>(content);
            logger.LogInformation($"Added post {content}");
            return result;
        }

        public BlogPostResponse UpdatePost(int id, string htmlContent, string title)
        {
            var post = new Post();
            post.postid = id;
            post.description = htmlContent;
            post.title = title;

            var input = JsonConvert.SerializeObject(post);
            var request = new StringContent(input, Encoding.UTF8, "application/json");
            var response = httpClient.PutAsync("/api/blogpost", request).Result;

            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<BlogPostResponse>(content);
            logger.LogInformation($"Updated {title}");
            return result;
        }

        public void Initialize(string server, string username, string password)
        {
            if (string.IsNullOrEmpty(server)) { throw new ArgumentException(nameof(server)); }
            if (string.IsNullOrEmpty(username)) { throw new ArgumentException(nameof(username)); }
            if (string.IsNullOrEmpty(password)) { throw new ArgumentException(nameof(password)); }

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
            logger.LogInformation($"Uploading {localName}");

            var input = JsonConvert.SerializeObject(media);
            var request = new StringContent(input, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync("/api/media", request).Result;

            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            logger.LogInformation($"Media upload returns {content}");
            var result = JsonConvert.DeserializeObject<MediaResponse>(content);

            logger.LogInformation($"Uploaded {localName}");
            return result.url;
        }
    }
}
