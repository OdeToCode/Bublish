using Bublish.Tests.Doubles;
using Microsoft.Extensions.Logging.Console;
using Xunit;

namespace Bublish.Tests.Startup
{
    public class ShellTests
    {
        [Fact]
        public void WillSetDefaults()
        {
            var shell = new Application(
                new TestLogger<Application>(), 
                new TestConverter(),
                new TestFileSystem(),
                new TestBlogServer());

            shell.SetDefaults();

            Assert.Equal("convert", shell.Action);
        }
    }
}
