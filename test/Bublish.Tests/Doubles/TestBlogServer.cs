using System;
using Bublish.Blog;

namespace Bublish.Tests.Doubles
{
    class TestBlogServer : IBlogServer
    {
        public bool Initialized { get; set; }

        public int AddPost()
        {
            throw new NotImplementedException();
        }

        public void Initialize(string server, string username, string password)
        {
            Initialized = true;
        }

        public string UploadMedia(string localName, byte[] bits)
        {
            return "https://server/images3/diagram";
        }
    }
}
