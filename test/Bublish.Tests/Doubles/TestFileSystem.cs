using Bublish.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bublish.Tests.Doubles
{
    class TestFileSystem : IFileSystem
    {
        private string textContent;

        public TestFileSystem(string textContent = null)
        {
            this.textContent = textContent;
        }

        public string ChangeExtension(string name, string newExtension)
        {
            return null;
        }
        
        public bool Exists(string name)
        {
            return true;
        }

        public List<string> FindFiles(string searchPattern)
        {
            return new List<string>();
        }
        
        public byte[] ReadBinary(string name)
        {
            return new Byte[] { 0x00, 0x01 };
        }

        public string ReadText(string name)
        {
            return textContent;
        }

        public void SetFolder(string name)
        {
            
        }

        public void WriteText(string name, string content)
        {
            textContent = content;
        }
    }
}
