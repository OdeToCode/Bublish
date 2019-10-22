using System;
using System.Collections.Generic;
using System.IO;

namespace Bublish.Files
{
    public class BlogPost
    {
        private string content;
        private readonly string name;
        private readonly IFileSystem fileSystem;        
        private readonly string[] metadataDelimeters = new string[] { "---", "..."  };
        private readonly IDictionary<string, string> metadata;

        public BlogPost(string name, IFileSystem fileSystem)
        {
            this.name = name;
            this.fileSystem = fileSystem;
            this.content = fileSystem.ReadText(name);
            this.metadata = ParseMetadata();
        }

        public DateTime PublishDate
        {
            get
            {
                if (metadata.ContainsKey("date"))
                {
                    return DateTime.Parse(metadata["date"]);
                }
                return DateTime.Now;
            }
        }

        public string Title
        {
            get
            {
                return metadata["title"];
            }
        }

        public int PostId
        {
            get
            {
                if (metadata.ContainsKey("id"))
                {
                    return int.Parse(metadata["id"]);
                }
                return 0;
            }

            set
            {
                if (metadata.ContainsKey("id"))
                {
                    metadata["id"] = value.ToString();
                }
                else
                {
                    metadata.Add("id", value.ToString());
                }

            }
        }

        public string Link
        {
            set
            {
                if (metadata.ContainsKey("link"))
                {
                    metadata["link"] = value;
                }
                else
                {
                    metadata.Add("link", value);
                }
            }
        }

        public void SaveContent()
        {
            using (var writer = new StringWriter())
            {
                writer.WriteLine(metadataDelimeters[0]);
                foreach (var kvp in metadata)
                {
                    writer.WriteLine($"{kvp.Key}:{kvp.Value}");
                }
                writer.Write(metadataDelimeters[0]);

                writer.Write(GetContentNoMetadata());
                content = writer.ToString();
                fileSystem.WriteText(name, content);
            }
        }

        public string GetContentNoMetadata()
        {
            var firstIndex = content.IndexOf(metadataDelimeters[0]);
            if(firstIndex < 0)
            {
                firstIndex = content.IndexOf(metadataDelimeters[1]);
            }

            var secondIndex = content.IndexOf(metadataDelimeters[0], firstIndex + 3);
            if(secondIndex < 0)
            {
                secondIndex = content.IndexOf(metadataDelimeters[1], firstIndex + 3);
            }
            return content.Substring(secondIndex + 3);
        }

        public string GetContent()
        {
            return content;
        }

        IDictionary<string, string> ParseMetadata()
        {
            var result = new Dictionary<string, string>();
            using (var reader = new StringReader(content))
            {
                var firstLine = reader.ReadLine();
                if (firstLine.StartsWith(metadataDelimeters[0]) || firstLine.StartsWith(metadataDelimeters[1]))
                {
                    var foundMetadataEnd = false;
                    while (!foundMetadataEnd)
                    {
                        var line = reader.ReadLine();
                        if (line.StartsWith(metadataDelimeters[0]) || line.StartsWith(metadataDelimeters[1]))
                        {
                            foundMetadataEnd = true;
                        }
                        else
                        {
                            var span = line.AsSpan();
                            var key = span.Slice(0, span.IndexOf(":"));
                            var value = span.Slice(span.IndexOf(":") + 1);
                            result.Add(key.ToString(), value.ToString());
                        }
                    }
                }
            }
            return result;
        }
    }
}
