using Bublish.Blog;
using Bublish.Files;
using Bublish.Markdig.Extensions;
using Markdig;

namespace Bublish.Markdig
{
    public class PipelineFactory : IPipelineFactory
    {
        private readonly IFileSystem fileSystem;
        private readonly IBlogServer blogServer;

        public PipelineFactory(IFileSystem fileSystem, IBlogServer blogServer)
        {
            this.fileSystem = fileSystem;
            this.blogServer = blogServer;
        }

        public MarkdownPipeline Build()
        {
            return new MarkdownPipelineBuilder()
                           .UseYamlFrontMatter()
                           .UsePreCode()
                           .UseImageProcessing(fileSystem, blogServer)
                           .UseGenericAttributes()           
                           .Build();
        }
    }
}
