using Bublish.Blog;
using Bublish.Files;
using Bublish.Markdig;
using Markdig;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace Bublish
{
    public class Application
    {
        private readonly ILogger<Application> logger;
        private readonly IFileSystem fileSystem;
        private readonly IBlogServer blogServer;
        private readonly IPipelineFactory pipelineFactory;

        public Application(ILogger<Application> logger, 
                     IFileSystem fileSystem,
                     IBlogServer blogServer,
                     IPipelineFactory pipelineFactory)
        {
            this.logger = logger;
            this.fileSystem = fileSystem;
            this.blogServer = blogServer;
            this.pipelineFactory = pipelineFactory;
        }

        [Option(Description = "Folder name with markdown files, defaults to current folder", 
                ShortName = "p")]
        [DirectoryExists]
        public string Path { get; protected set; }

        [Option(Description = "Base server address",
                ShortName = "s")]
        public string Server { get; set; }

        [Option(Description = "Username",
                ShortName = "u")]
        public string Username { get; set; }

        [Option(Description = "Password", ShortName = "pw")]
        public string Password { get; set; }
     
        public void OnExecute()
        {
            SetDefaults();
            RunActions();
        }

        public void RunActions()
        {
            blogServer.Initialize(Server, Username, Password);

            var pipeline = pipelineFactory.Build();
            var files = fileSystem.FindFiles("*.md");
            foreach (var file in files)
            {
                var post = new BlogPost(file, fileSystem);
                var html = Markdown.ToHtml(post.GetContent(), pipeline);

                var newName = fileSystem.ChangeExtension(file, ".html");
                fileSystem.WriteText(newName, html);

                var response = (BlogPostResponse)null;
                if(post.PostId < 1)
                {
                    response = blogServer.AddPost(post.PublishDate, html, post.Title, slug: null);
                }
                else
                {
                    response = blogServer.UpdatePost(post.PostId, html, post.Title);
                }

                post.PostId = response.Id;
                post.Link = response.Permalink;
                post.SaveContent();
            }
        }

        public void SetDefaults()
        {
            Path = Path ?? Directory.GetCurrentDirectory();
            Server = Server ?? "https://odetocode.com";
            Username = Username ?? "scott";

            fileSystem.SetFolder(Path);
        }
    }
}
