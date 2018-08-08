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
    }
}
