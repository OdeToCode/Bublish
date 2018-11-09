using Bublish.Markdig.Extensions;
using Bublish.Tests.Doubles;
using Markdig;
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
![diagram](/somediagram.png)
More text
<img width='636' height='687' align='right' style='margin: 0px 0px 10px 10px; float: right; display: inline; background - image: none; ' alt='Azure Application Insights Extension' src='diagram.png' border='0'>
";
            var pipeline =
                   new MarkdownPipelineBuilder()
                           .UsePreCode()
                           .UseImageProcessing(fs, bs)
                           .Build();

            var html = Markdown.ToHtml(text, pipeline);

            Assert.Contains("src=\"https://server/images3/diagram\"", html);
        }
    }
}
