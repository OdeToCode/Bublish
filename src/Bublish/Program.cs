using Bublish.Conversion;
using Bublish.Files;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bublish
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = BuildServices();
            var console = new CommandLineApplication<Shell>();
            console.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(provider);

            console.Execute(args);
        }

        public static ServiceProvider BuildServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(c => c.AddConsole());
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<IMarkdownToHtml, MarkdownToHtml>();

            return services.BuildServiceProvider();
        }
    }
}