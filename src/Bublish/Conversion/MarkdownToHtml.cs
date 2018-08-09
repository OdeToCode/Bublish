using Bublish.Files;
using Bublish.Markdig;
using Markdig;
using Microsoft.Extensions.Logging;

namespace Bublish.Conversion
{
    public interface IMarkdownToHtml
    {
        void Convert();
    }

    public class MarkdownToHtml : IMarkdownToHtml
    {
        private readonly ILogger logger;
        private readonly IFileSystem fileSystem;

        public MarkdownToHtml(ILogger<IMarkdownToHtml> logger, IFileSystem fileSystem)
        {
            this.logger = logger;
            this.fileSystem = fileSystem;
        }

        public void Convert()
        {
            var files = fileSystem.FindFiles("*.md");
            var pipeline = new BublishPipeline().Build();

            foreach(var file in files)
            {
                logger.LogInformation($"Converting {file}");

                var markdown = fileSystem.ReadFile(file);
                var html = Markdown.ToHtml(markdown, pipeline);
                var newName = fileSystem.ChangeExtension(file, ".html");
                fileSystem.WriteFile(newName, html);
            }
        }
    }
}
