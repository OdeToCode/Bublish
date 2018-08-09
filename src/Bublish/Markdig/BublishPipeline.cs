using Bublish.Markdig.Extensions;
using Markdig;

namespace Bublish.Markdig
{
    public class BublishPipeline
    {
        public MarkdownPipeline Build()
        {
            return new MarkdownPipelineBuilder()
                           .UsePreCode()
                           .Build();
        }
    }
}
