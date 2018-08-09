using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Bublish
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var provider = services.BuildServiceProvider();
            var console = new CommandLineApplication<Program>();
            console.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(provider);

            CommandLineApplication.Execute<Program>(args);
        }
            
        [Option(Description = "Folder name with markdown files, defaults to current folder", ShortName ="p")]
        public string Path { get; protected set; } 

        [Argument(0)]
        [AllowedValues("convert")]
        public string Action { get; set; }

        public Program()
        {

        }

        public void OnExecute()
        {
            SetDefaults();
            RunConvert();
            
        }

        public void RunConvert()
        {
            if(Action == "convert")
            {

            }
        }

        public void SetDefaults()
        {
            Action = Action ?? "convert";
            Path = Path ?? Directory.GetCurrentDirectory();
        }
    }
}
