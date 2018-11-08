using System;
using System.Linq;
using Bublish.Blog;
using Bublish.Files;
using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Bublish.Markdig.Extensions
{
    public class ImageProcessingExtension : IMarkdownExtension
    {
        private readonly IFileSystem fileSystem;
        private readonly IBlogServer blogServer;

        public ImageProcessingExtension(IFileSystem fileSystem, IBlogServer blogServer)
        {
            this.fileSystem = fileSystem;
            this.blogServer = blogServer;
        }

        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            pipeline.DocumentProcessed += Pipeline_DocumentProcessed;
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {

        }

        private void Pipeline_DocumentProcessed(MarkdownDocument document)
        {
            foreach(var node in document.Descendants()
                                        .OfType<LinkInline>()
                                        .Where(l => l.IsImage))
            {
                var localName = node.Url;
                if (!fileSystem.Exists(localName))
                {
                    throw new ArgumentException($"Cannot find file {localName}");
                }

                var bytes = fileSystem.ReadBinary(localName);
                var newUrl = blogServer.UploadMedia(localName, bytes);

                node.Url = newUrl;
            }
        }      
    }
}
