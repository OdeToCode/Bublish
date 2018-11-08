using Bublish.Blog;
using Bublish.Conversion;
using Bublish.Files;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Bublish
{
    public class Application
    {
        private readonly ILogger<Application> logger;
        private readonly IMarkdownToHtml converter;
        private readonly IFileSystem fileSystem;
        private readonly IBlogServer blogServer;

        public Application(ILogger<Application> logger, 
                     IMarkdownToHtml converter, 
                     IFileSystem fileSystem,
                     IBlogServer blogServer)
        {
            this.logger = logger;
            this.converter = converter;
            this.fileSystem = fileSystem;
            this.blogServer = blogServer;
        }

        [Option(Description = "Folder name with markdown files, defaults to current folder", ShortName = "p")]
        [DirectoryExists]
        public string Path { get; protected set; }

        [Option(Description = "Base server address")]
        public string Server { get; set; }

        [Option(Description = "Username")]
        public string Username { get; set; }

        [Option(Description = "Password")]
        public string Password { get; set; }

        [Argument(0, Description ="convert")]
        [AllowedValues("convert | publish")]
        public string Action { get; set; }
     
        public void OnExecute()
        {
            SetDefaults();
            RunActions();
        }

        public void RunActions()
        {
            if (Action.Contains("convert") || Action.Contains("publish"))
            {
                logger.LogInformation("Beginning conversion");
                converter.Convert();
                logger.LogInformation("Finished conversion");
            }
            if (Action.Contains("publish"))
            {
                logger.LogInformation("Beginning publish");
                blogServer.Initialize(Server, Username, Password);
                logger.LogInformation("Publish finished");
            }
        }

        public void SetDefaults()
        {
            Action = Action ?? "convert";
            Path = Path ?? Directory.GetCurrentDirectory();

            fileSystem.SetFolder(Path);
        }
    }
}
