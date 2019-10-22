using Bublish.Markdig.Extensions;
using Bublish.Tests.Doubles;
using Markdig;
using System.Text.RegularExpressions;
using Xunit;

namespace Bublish.Tests
{
    public class MarkdownWithImageTests
    { 
        [Fact]
        public void UploadsImagesAndAddsUrlToHtml()
        {
            var fs = new TestFileSystem();
            var bs = new TestBlogServer();

            var text =
@"# Hello 
Some text
![diagram](/somediagram.png){ .image-center }
More text
";
            var pipeline =
                   new MarkdownPipelineBuilder()
                           .UsePreCode()
                           .UseImageProcessing(fs, bs)
                           .UseGenericAttributes()
                           .Build();

            var html = Markdown.ToHtml(text, pipeline);

            Assert.Contains("https://server/images3/diagram", html);
            Assert.Contains("class=\"image-center\"", html);
        }
    }
}
