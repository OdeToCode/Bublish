using Bublish.Blog;
using Bublish.Files;
using Bublish.Markdig;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bublish
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = ConfigureServices();

            var app = new CommandLineApplication<Application>();
            app.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(provider);

            app.Execute(args);
        }

        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(c => c.AddConsole());
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<IBlogServer, BlogServer>();
            services.AddSingleton<IPipelineFactory, PipelineFactory>();
            
            return services.BuildServiceProvider();
        }
    }
}