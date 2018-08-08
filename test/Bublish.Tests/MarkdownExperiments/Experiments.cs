using Markdig;
using Xunit;

namespace Bublish.Tests.MarkdownExperiments
{
    public class Experiments
    {
        [Fact]
        public void CanParseSimpleMarkdown()
        {
            var text = "### Hello World!";
            var html = Markdown.ToHtml(text);

            Assert.Equal("<h3>Hello World!</h3>\n", html);
        }

           
        [Fact]
        public void CanFindCode()
        {
            var text =
                @"```csharp
                   public static void Main(string[] args) { }    
                ```";

            var pipeline = 
                    new MarkdownPipelineBuilder()
                            .
            var html = Markdown.ToHtml(text);

            Assert.Equal(@"<pre class=""brush: csharp; gutter: false; toolbar: false; "">\npublic static void Main(string[] args) { }\n</pre >\n", html);
        }
    }
}
