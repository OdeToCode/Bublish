using System.Collections.Generic;

namespace Bublish.Files
{
    public interface IFileSystem
    {
        List<string> FindFiles(string searchPattern);
        string ReadText(string name);
        byte[] ReadBinary(string name);
        void WriteText(string name, string content);
        void SetFolder(string name);
        string ChangeExtension(string name, string newExtension);
        bool Exists(string name);
    }
}