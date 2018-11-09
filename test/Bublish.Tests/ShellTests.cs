using Bublish.Markdig;
using Bublish.Tests.Doubles;
using Xunit;

namespace Bublish.Tests
{
    public class ShellTests
    {
        [Fact]
        public void WillSetDefaults()
        {
            var fs = new TestFileSystem();
            var bs = new TestBlogServer();
            var shell = new Application(
                new TestLogger<Application>(),
                fs,
                bs,
                new PipelineFactory(fs, bs));

            shell.SetDefaults();

            Assert.True(!string.IsNullOrEmpty(shell.Path));
        }
    }
}
