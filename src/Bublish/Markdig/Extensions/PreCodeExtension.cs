using Bublish.Markdig.Renderers;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Bublish.Markdig.Extensions
{
    public class PreCodeExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            var htmlRenderer = (HtmlRenderer)renderer;

            var originalCodeBlockRenderer = htmlRenderer.ObjectRenderers.FindExact<CodeBlockRenderer>();
            if (originalCodeBlockRenderer != null)
            {
                htmlRenderer.ObjectRenderers.Remove(originalCodeBlockRenderer);
            }

            htmlRenderer.ObjectRenderers.AddIfNotAlready(
                new PreCodeRenderer(originalCodeBlockRenderer));
        }
    }
}