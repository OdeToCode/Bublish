using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bublish.Files
{
    public class FileSystem : IFileSystem
    {
        private string folder;

        public FileSystem()
        {
            this.folder = Directory.GetCurrentDirectory();
        } 

        public void SetFolder(string folder)
        {
            this.folder = folder;
        }

        public List<string> FindFiles(string searchPattern)
        {
            var files = Directory.GetFiles(folder, searchPattern);
            return files.ToList();
        }

        public string ReadFile(string name)
        {
            var result = File.ReadAllText(Path.Combine(folder, name));
            return result;        
        }

        public void WriteFile(string name, string content)
        {
            File.WriteAllText(Path.Combine(folder, name), content);
        }

        public string ChangeExtension(string name, string newExtension)
        {
            return Path.ChangeExtension(name, newExtension);
        }
    }
}