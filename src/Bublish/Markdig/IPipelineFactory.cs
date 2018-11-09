using Markdig;

namespace Bublish.Markdig
{
    public interface IPipelineFactory
    {
        MarkdownPipeline Build();
    }
}