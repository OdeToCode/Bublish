using Bublish.Markdig.Extensions;
using Bublish.Markdig.Renderers;
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using System.IO;
using System.Linq;
using Xunit;

namespace Bublish.Tests.MarkdownExperiments
{
    public class MarkdownTests
    {
        [Fact]
        public void CanParseSimpleMarkdown()
        {
            var text = "### Hello World!";
            var html = Markdown.ToHtml(text);

            Assert.Equal("<h3>Hello World!</h3>\n", html);
        }

        [Fact]
        public void UsePreCodeAddsExtension()
        {
            var pipeline = new MarkdownPipelineBuilder()
                                .UsePreCode()
                                .Build();

            Assert.Single(pipeline.Extensions.OfType<PreCodeExtension>());                           
        }

        [Fact]
        public void PreCodeExtensionAddsRenderer()
        {
            var extension = new PreCodeExtension();
            var pipeline = new MarkdownPipelineBuilder().Build();
            var renderer = new HtmlRenderer(new StringWriter());

            extension.Setup(pipeline, renderer);

            Assert.Empty(renderer.ObjectRenderers.OfType<CodeBlockRenderer>());
            Assert.Single(renderer.ObjectRenderers.OfType<PreCodeRenderer>());
        }
           
        [Fact]
        public void CanCreateCodeBlocks()
        {
            var text =
                @"```csharpy
                    public static void Main(string[] args) { }    
                ```";

            var pipeline =
                    new MarkdownPipelineBuilder()
                            .UsePreCode()
                            .Build();

            var html = Markdown.ToHtml(text, pipeline);

            Assert.Contains("<pre class=\"brush: csharpy; gutter: false; toolbar: false; \">", html);
            Assert.Contains("public static void Main", html);
        }
    }
}
