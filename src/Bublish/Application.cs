using Bublish.Conversion;
using Bublish.Files;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Bublish
{
    public class Application
    {
        private readonly ILogger<Application> logger;
        private readonly IMarkdownToHtml converter;
        private readonly IFileSystem fileSystem;

        public Application(ILogger<Application> logger, 
                     IMarkdownToHtml converter, 
                     IFileSystem fileSystem)
        {
            this.logger = logger;
            this.converter = converter;
            this.fileSystem = fileSystem;
        }

        [Option(Description = "Folder name with markdown files, defaults to current folder", ShortName = "p")]
        [DirectoryExists]
        public string Path { get; protected set; }

        [Argument(0, Description ="convert | publish | clean")]
        [AllowedValues("convert")]
        public string Action { get; set; }
     
        public void OnExecute()
        {
            SetDefaults();
            RunConvert();
        }

        public void RunConvert()
        {
            if (Action == "convert")
            {
                logger.LogInformation("Beginning conversion");
                converter.Convert();
                logger.LogInformation("Finished conversion");
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
