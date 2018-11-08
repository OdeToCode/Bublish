using Bublish.Blog;
using Bublish.Files;
using Markdig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bublish.Markdig.Extensions
{
    public static class PipelineBuilderExtensions
    {
        public static MarkdownPipelineBuilder UsePreCode(this MarkdownPipelineBuilder builder)
        {
            builder.Extensions.AddIfNotAlready<PreCodeExtension>();
            return builder;
        }

        public static MarkdownPipelineBuilder UseImageProcessing(
            this MarkdownPipelineBuilder builder, 
            IFileSystem fileSystem, IBlogServer blogServer)
        {
            var extension = new ImageProcessingExtension(fileSystem, blogServer);
            builder.Extensions.AddIfNotAlready(extension);
            return builder;
        }
    }
}
