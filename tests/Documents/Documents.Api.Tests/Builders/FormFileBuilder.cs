using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Text;

namespace Document.Api.Tests.Builders
{
    internal class FormFileBuilder
    {
        private string? _fileName;
        private string? _content;

        public FormFileBuilder WithFileName(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public FormFileBuilder WithContent(string content)
        {
            _content = content;
            return this;
        }

        public FormFileBuilder WithFullObject()
        {
            WithFileName("filename.txt");
            WithContent("File content");
            return this;
        }

        public IFormFile Build()
        {
            var file = Substitute.For<IFormFile>();
            file.FileName.Returns(_fileName);

            if (_content != null)
                file.OpenReadStream().Returns(new MemoryStream(Encoding.UTF8.GetBytes(_content)));
            return file;
        }
    }
}
