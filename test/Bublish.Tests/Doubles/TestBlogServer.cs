using System;
using Bublish.Blog;

namespace Bublish.Tests.Doubles
{
    class TestBlogServer : IBlogServer
    {
        public bool Initialized { get; set; }


        public BlogPostResponse AddPost(DateTime publishDate, string htmlContent, string title, string slug)
        {
            throw new NotImplementedException();
        }

        public void Initialize(string server, string username, string password)
        {
            Initialized = true;
        }

        public BlogPostResponse UpdatePost(int id, string htmlContent, string title)
        {
            throw new NotImplementedException();
        }

        public string UploadMedia(string localName, byte[] bits)
        {
            return "https://server/images3/diagram";
        }
    }
}
