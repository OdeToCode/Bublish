using Bublish.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bublish.Tests.Doubles
{
    class TestFileSystem : IFileSystem
    {
        public string ChangeExtension(string name, string newExtension)
        {
            return null;
        }

        public List<string> FindFiles(string searchPattern)
        {
            return new List<string>();
        }

        public string ReadFile(string name)
        {
            return null;
        }

        public void SetFolder(string name)
        {
            
        }

        public void WriteFile(string name, string content)
        {
            
        }
    }
}
