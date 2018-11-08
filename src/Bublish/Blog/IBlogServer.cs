using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bublish.Blog
{
    public interface IBlogServer
    {
        void Initialize(string server, string username, string password);
        string UploadMedia(string localName, byte[] bits);
        int AddPost();
    }
}
