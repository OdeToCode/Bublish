using System;
using Bublish.Files;
using Bublish.Tests.Doubles;
using Xunit;

namespace Bublish.Tests
{
    public class BlogPostTests
    {
        [Fact]
        public void CanGetContent()
        {
            var content = "are we in trouble now?";
            var fs = new TestFileSystem(textContent: content);

            var post = new BlogPost("blog.md", fs);
            var markdown = post.GetContent();

            Assert.Equal(content, markdown);
        }

        [Fact]
        public void CanGetPublishDate()
        {
            var content =
@"---
date: 11/15/2017 10:03am
---
quack quack 
";
            var fs = new TestFileSystem(textContent: content);

            var post = new BlogPost("blog.md", fs);
            var date = post.PublishDate;

            Assert.Equal(new DateTime(2017, 11, 15, 10, 03, 0), date);
        }

        [Fact]
        public void PostIdDefaultsToZero()
        {
            var content =
@"---
date: 11/15/2017 10:03am
...
quack quack 
";
            var fs = new TestFileSystem(textContent: content);

            var post = new BlogPost("blog.md", fs);
            var id = post.PostId;

            Assert.Equal(0, id);
        }

        [Fact]
        public void CanGetPostId()
        {
            var content =
@"---
date: 11/15/2017 10:03am
id: 42
...
quack quack 
";
            var fs = new TestFileSystem(textContent: content);

            var post = new BlogPost("blog.md", fs);
            var id = post.PostId;

            Assert.Equal(42, id);
        }

        [Fact]
        public void CanUpdatePostId()
        {
            var content =
@"---
date: 11/15/2017 10:03am
...
quack quack 
";
            var fs = new TestFileSystem(textContent: content);

            var post = new BlogPost("blog.md", fs);
            post.PostId = 42;
            post.SaveContent();

            var result = fs.ReadText("_");
            Assert.Contains("id:42", result);
            Assert.EndsWith("---\r\nquack quack \r\n", result);
        }
    }
}
