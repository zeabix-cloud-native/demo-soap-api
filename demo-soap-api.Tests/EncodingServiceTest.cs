using System.Text;
using DataEncodingApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataEncodingApi.Interfaces.IServices;
using Xunit;

namespace DataEncodingApi.Tests
{
    public class EncodingServiceTest
    {
        private readonly DataEncodingController _controller;
        private readonly ILogger<DataEncodingController> _logger;

        public EncodingServiceTest()
        {
            // Mock ILogger
            _logger = new LoggerFactory().CreateLogger<DataEncodingController>();
            _controller = new DataEncodingController(_logger);
        }

        [Fact]
        public void EncodeToUtf8_ShouldReturnCorrectEncodedString()
        {
            // Arrange
            var input = "สวัสดีชาวไทย";
            var expectedUtf8Bytes = Encoding.UTF8.GetBytes(input);
            var expectedUtf8String = Encoding.UTF8.GetString(expectedUtf8Bytes);

            // Act
            var result = ((IEncodingService)_controller).EncodeToUtf8(input);

            // Assert
            Assert.Equal(expectedUtf8String, result);
        }

        // [Fact]
        // public void EncodeToTis620_ShouldReturnCorrectEncodedString()
        // {
        //     // Arrange
        //     var input = "สวัสดีชาวไทย";
        //     Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //     var tis620 = Encoding.GetEncoding("TIS-620");
        //     var utf8 = Encoding.UTF8;

        //     var tis620Bytes = tis620.GetBytes(input);
        //     var utf8Bytes = Encoding.Convert(tis620, utf8, tis620Bytes);
        //     var expectedUtf8String = Encoding.UTF8.GetString(utf8Bytes);

        //     // Act
        //     var result = ((IEncodingService)_controller).EncodeToTis620(input);

        //     // Assert
        //     Assert.Equal(expectedUtf8String, result);
        // }

        // [Fact]
        // public void ConvertTis620ToUtf8File_ShouldReturnFileContentResult()
        // {
        //     // Act
        //     var result = _controller.ConvertTis620ToUtf8File() as FileContentResult;

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal("text/plain; charset=utf-8", result.ContentType);
        //     Assert.Equal("ConvertedToUtf8.txt", result.FileDownloadName);

        //     // Verify the file content
        //     var utf8 = Encoding.UTF8;
        //     var tis620 = Encoding.GetEncoding("TIS-620");
        //     var tis620Bytes = tis620.GetBytes("สวัสดี");
        //     var expectedUtf8Bytes = Encoding.Convert(tis620, utf8, tis620Bytes);

        //     Assert.Equal(expectedUtf8Bytes, result.FileContents);
        // }
    }
}
