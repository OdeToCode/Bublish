using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using System;
using System.Collections.Generic;

namespace Bublish.Markdig.Renderers
{
    public class PreCodeRenderer : HtmlObjectRenderer<CodeBlock>
    {
        private CodeBlockRenderer originalCodeBlockRenderer;
        
        public PreCodeRenderer(CodeBlockRenderer originalCodeBlockRenderer = null)
        {
            this.originalCodeBlockRenderer = originalCodeBlockRenderer ?? new CodeBlockRenderer();
        }

        public bool OutputAttributesOnPre { get; set; }

        protected override void Write(HtmlRenderer renderer, CodeBlock obj)
        {
            renderer.EnsureLine();
        
            var fencedCodeBlock = obj as FencedCodeBlock;
            if (fencedCodeBlock?.Info != null)
            {
                renderer.Write($"<pre class=\"brush: {fencedCodeBlock.Info}; gutter: false; toolbar: false; \">");
                renderer.EnsureLine();
                renderer.WriteLeafRawLines(obj, true, true);
                renderer.WriteLine("</pre>");
            }
            else
            {
                originalCodeBlockRenderer.Write(renderer, obj);
            }                
        }
    }
}
