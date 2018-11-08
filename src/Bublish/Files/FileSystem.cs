using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bublish.Files
{
    public class FileSystem : IFileSystem
    {
        private string folder;
        private readonly ILogger<FileSystem> logger;

        public FileSystem(ILogger<FileSystem> logger)
        {
            this.folder = Directory.GetCurrentDirectory();
            this.logger = logger;
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

        public string ReadText(string name)
        {
            var fullPath = Path.Combine(folder, name);
            var result = File.ReadAllText(fullPath);
            return result;        
        }

        public byte[] ReadBinary(string name)
        {
            var fullPath = Path.Combine(folder, name);
            var result = File.ReadAllBytes(fullPath);
            return result;
        }

        public void WriteText(string name, string content)
        {           
            var fullPath = Path.Combine(folder, name);
            logger.LogInformation($"Writing to {fullPath}");
            File.WriteAllText(fullPath, content);
        }

        public bool Exists(string name)
        {
            return File.Exists(Path.Combine(folder, name));
        }

        public string ChangeExtension(string name, string newExtension)
        {
            return Path.ChangeExtension(name, newExtension);
        }
    }
}