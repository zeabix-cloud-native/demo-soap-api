// using System.Text;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using DataEncodingApi.Interfaces.IServices;

// namespace DataEncodingApi.Controllers
// {
//     [ApiController]
//     [Route("dataEncoding")]
//     public class DataEncodingController : ControllerBase, IEncodingService
//     {
//         private readonly ILogger<DataEncodingController> _logger;

//         static DataEncodingController()
//         {
//             Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
//         }

//         public DataEncodingController(ILogger<DataEncodingController> logger)
//         {
//             _logger = logger;
//         }
//         string IEncodingService.EncodeToUtf8(string input)
//         {
//             return EncodeUtf8Internal(input);
//         }

//         string IEncodingService.EncodeToTis620(string input)
//         {
//             return EncodeTis620Internal(input);
//         }

//         [HttpPost("encodeToUtf8")]
//         public IActionResult EncodeToUtf8()
//         {
//             string textFixed = "สวัสดีชาวไทย";
//             string textFixedEng = "Hello Thai people";
//             string encodedString = EncodeUtf8Internal(textFixed);
//             string encodedStringEng = EncodeUtf8Internal(textFixed);
//             return CreateSoapResponse("EncodeToUtf8", encodedString, encodedStringEng ,"utf-8");
//         }

//         [HttpPost("encodeToTis620")]
//         public IActionResult EncodeToTis620()
//         {
//             string textFixed = "สวัสดีชาวไทย";
//             string textFixedEng = "Hello Thai people";
//             string encodedString = EncodeTis620Internal(textFixed);
//             string encodedStringEng = EncodeTis620Internal(textFixedEng);
//             return CreateSoapResponse("EncodeToTis620", encodedString,encodedStringEng , "TIS-620");
//         }

       
//         [HttpPost("test")]
//         public IActionResult ConvertTis620ToUtf8()
//         {
//             Encoding tis620 = Encoding.GetEncoding("TIS-620");
//             Encoding utf8 = Encoding.UTF8;

//             byte[] tis620Bytes = tis620.GetBytes("สวัสดี");

//             byte[] utf8Bytes = Encoding.Convert(tis620, utf8, tis620Bytes);
//             return new FileContentResult(utf8Bytes, "text/plain; charset=tis-620");
//         }


//             // return new FileContentResult(tis620Bytes, "text/plain; charset=tis-620");

//         private string EncodeUtf8Internal(string input)
//         {
//             byte[] utf8Bytes = Encoding.UTF8.GetBytes(input);
//             return Encoding.UTF8.GetString(utf8Bytes);
//         }

//         private string EncodeTis620Internal(string input)
//         {
//             Encoding tis620 = Encoding.GetEncoding("TIS-620");
//             byte[] tis620Bytes = tis620.GetBytes(input);
//             return tis620.GetString(tis620Bytes);
//         }

//         private ContentResult CreateSoapResponse(string methodName, string encodedInput,  string encodedInputEng, string charset)
//         {
//             string safeEncodedInput = System.Security.SecurityElement.Escape(encodedInput);
//             string safeEncodedInputEng = System.Security.SecurityElement.Escape(encodedInputEng);

//             string responseXml = $@"
//             <s:Envelope xmlns:s='http://schemas.xmlsoap.org/soap/envelope/'>
//             <s:Body>
//                 <{methodName}Response xmlns='http://tempuri.org/'>
//                     <{methodName}Result>
//                         <EncodedString>{safeEncodedInput}</EncodedString>
//                         <EncodedStringEng>{safeEncodedInputEng}</EncodedStringEng>
//                     </{methodName}Result>
//                 </{methodName}Response>
//             </s:Body>
//             </s:Envelope>";

//             return new ContentResult
//             {
//                 Content = responseXml,
//                 ContentType = $"text/xml; charset={charset}",
//                 StatusCode = 200
//             };
//         }
//     }
// }
