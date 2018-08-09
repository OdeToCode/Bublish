using System.Collections.Generic;

namespace Bublish.Files
{
    public interface IFileSystem
    {
        List<string> FindFiles(string searchPattern);
        string ReadFile(string name);
        void WriteFile(string name, string content);
        void SetFolder(string name);
        string ChangeExtension(string name, string newExtension);
    }
}