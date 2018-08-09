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
            var shell = new Shell(
                new TestLogger<Shell>(), 
                new TestConverter(),
                new TestFileSystem());

            shell.SetDefaults();

            Assert.Equal("convert", shell.Action);
        }
    }
}
