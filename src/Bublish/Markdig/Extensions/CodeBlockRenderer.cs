using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using System;
using System.Collections.Generic;

namespace Bublish.Markdig.Renderers
{
    public class CodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
    {
        public CodeBlockRenderer()
        {
            BlocksAsDiv = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public bool OutputAttributesOnPre { get; set; }

        public HashSet<string> BlocksAsDiv { get; }

        protected override void Write(HtmlRenderer renderer, CodeBlock obj)
        {
            renderer.EnsureLine();

            var fencedCodeBlock = obj as FencedCodeBlock;
            if (fencedCodeBlock?.Info != null && BlocksAsDiv.Contains(fencedCodeBlock.Info))
            {
                var infoPrefix = (obj.Parser as FencedCodeBlockParser)?.InfoPrefix ??
                                 FencedCodeBlockParser.DefaultInfoPrefix;

                if (renderer.EnableHtmlForBlock)
                {
                    renderer.Write("<div")
                            .WriteAttributes(obj.TryGetAttributes(),
                                cls => cls.StartsWith(infoPrefix) ? cls.Substring(infoPrefix.Length) : cls)
                            .Write(">");
                }

                renderer.WriteLeafRawLines(obj, true, true, true);

                if (renderer.EnableHtmlForBlock)
                {
                    renderer.WriteLine("</div>");
                }

            }
            else
            {
                if (renderer.EnableHtmlForBlock)
                {
                    renderer.Write("<pre");

                    if (OutputAttributesOnPre)
                    {
                        renderer.WriteAttributes(obj);
                    }

                    renderer.Write("><code");

                    if (!OutputAttributesOnPre)
                    {
                        renderer.WriteAttributes(obj);
                    }

                    renderer.Write(">");
                }

                renderer.WriteLeafRawLines(obj, true, true);

                if (renderer.EnableHtmlForBlock)
                {
                    renderer.WriteLine("</code></pre>");
                }
            }
        }
    }
}
