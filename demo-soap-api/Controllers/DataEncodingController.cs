using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataEncodingApi.Interfaces.IServices;

namespace DataEncodingApi.Controllers
{
    [ApiController]
    [Route("dataEncoding")]
    public class DataEncodingController : ControllerBase, IEncodingService
    {
        private readonly ILogger<DataEncodingController> _logger;

        static DataEncodingController()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public DataEncodingController(ILogger<DataEncodingController> logger)
        {
            _logger = logger;
        }

        string IEncodingService.EncodeToUtf8(string input)
        {
            return EncodeUtf8Internal(input);
        }

        // string IEncodingService.EncodeToTis620(string input)
        // {
        //     return EncodeTis620Internal(input);
        // }

        [HttpPost("encodeToUtf8")]
        public IActionResult EncodeToUtf8()
        {
            string textFixed = "สวัสดีชาวไทย";
            string textFixedEng = "Hello Thai people";
            string encodedString = EncodeUtf8Internal(textFixed);
            string encodedStringEng = EncodeUtf8Internal(textFixedEng);
            return CreateSoapResponse("EncodeToUtf8", encodedString, encodedStringEng, "utf-8");
        }

        // [HttpPost("encodeToTis620")]
        // public IActionResult EncodeToTis620()
        // {
        //     string textFixed = "สวัสดีชาวไทย";
        //     string textFixedEng = "Hello Thai people";
        //     string encodedString = EncodeTis620Internal(textFixed);
        //     string encodedStringEng = EncodeTis620Internal(textFixedEng);
        //     return CreateSoapResponse("EncodeToTis620", encodedString, encodedStringEng, "utf-8");
        // }

        private string EncodeUtf8Internal(string input)
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(input);
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        // private string EncodeTis620Internal(string input)
        // {   
        //     Encoding tis620 = Encoding.GetEncoding("TIS-620");
        //     Encoding utf8 = Encoding.UTF8;

        //     byte[] tis620Bytes = tis620.GetBytes(input);
        //     byte[] utf8Bytes = Encoding.Convert(tis620, utf8, tis620Bytes);

        //     string result = Encoding.UTF8.GetString(utf8Bytes);

        //     return result;
        // }

        // [HttpPost("convertTis620ToUtf8File")]
        // public IActionResult ConvertTis620ToUtf8File()
        // {
        //     Encoding tis620 = Encoding.GetEncoding("TIS-620");
        //     Encoding utf8 = Encoding.UTF8;

        //     byte[] tis620Bytes = tis620.GetBytes("สวัสดี");
        //     byte[] utf8Bytes = Encoding.Convert(tis620, utf8, tis620Bytes);

        //     return new FileContentResult(utf8Bytes, "text/plain; charset=utf-8")
        //     {
        //         FileDownloadName = "ConvertedToUtf8.txt"
        //     };
        // }

        private ContentResult CreateSoapResponse(string methodName, string encodedInput, string encodedInputEng, string charset)
        {
            string safeEncodedInput = System.Security.SecurityElement.Escape(encodedInput);
            string safeEncodedInputEng = System.Security.SecurityElement.Escape(encodedInputEng);

            string responseXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" 
                        xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                        xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                <soap:Body>
                    <ClaimsKnockReqResponse xmlns=""https://www.bangkokinsurance.com/RECOVERY_GATEWAY_EX_WS/S_CheckClaimKnock.asmx"">
                        <ClaimsKnockReqResult>
                            <RecordGUID>27675a23-439a-bf01-e063-dbf01e063c32810ac98ba</RecordGUID>
                            <SPName>BKI</SPName>
                            <PolicyNumber>622-70331-2770</PolicyNumber>
                            <EffectiveDt>2022-01-16T00:00:00</EffectiveDt>
                            <ExpirationDt>2023-01-16T00:00:00</ExpirationDt>
                            <PersonName>-</PersonName>
                            <PolicyTypeCd>1</PolicyTypeCd>
                            <VehTypeCd>110</VehTypeCd>
                            <Registration>2ขพ6552 กท</Registration>
                            <Manufacturer>HONDA</Manufacturer>
                            <Model>JAZZ 1.5  (NEW) S CVT (2015-)</Model>
                            <ChassisSerialNumber>MRHGK5830MT107486</ChassisSerialNumber>
                            <EngineSerialNumber>L15Z24619429</EngineSerialNumber>
                            <Displacement>1500</Displacement>
                            <GrossVehOrCombinedWeight>0</GrossVehOrCombinedWeight>
                            <SeatingCapacity>7</SeatingCapacity>
                            <Coverages>
                                <Coverage>
                                    <CoverageCd>LPD</CoverageCd>
                                    <CoverageDesc>ความรับผิดต่อทรัพย์สิน</CoverageDesc>
                                    <Limit>
                                        <LimitAmt>5000000</LimitAmt>
                                    </Limit>
                                </Coverage>
                                <Coverage>
                                    <CoverageCd>COLL</CoverageCd>
                                    <CoverageDesc>ความเสียหายต่อรถยนต์</CoverageDesc>
                                    <Limit>
                                        <LimitAmt>500000</LimitAmt>
                                    </Limit>
                                </Coverage>
                                <Coverage>
                                    <CoverageCd>FT</CoverageCd>
                                    <CoverageDesc>รถยนต์สูญหาย/ไฟไหม้</CoverageDesc>
                                    <Limit>
                                        <LimitAmt>500000</LimitAmt>
                                    </Limit>
                                </Coverage>
                            </Coverages>
                            <ClaimOccurences>
                                <ClaimOccurence>
                                    <ClaimNoticeCd>CLAIM</ClaimNoticeCd>
                                    <ItemIdInfo>2301331966</ItemIdInfo>
                                    <NotifyNumber>2023002262</NotifyNumber>
                                    <LossDt>2023-01-03T00:00:00</LossDt>
                                    <KfkStatus>Y</KfkStatus>
                                    <ReserveAmt>10276.70</ReserveAmt>
                                    <PaymentAmt>10276.70</PaymentAmt>
                                    <PaymentTypeCd>Final</PaymentTypeCd>
                                    <InDisputeInd>Y</InDisputeInd>
                                </ClaimOccurence>
                            </ClaimOccurences>
                            <TransactionResponseDt>{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss}</TransactionResponseDt>
                            <MsgStatusCd>Success</MsgStatusCd>
                        </ClaimsKnockReqResult>
                    </ClaimsKnockReqResponse>
                </soap:Body>
            </soap:Envelope>";

            return new ContentResult
            {
                Content = responseXml,
                ContentType = $"text/xml; charset={charset}",
                StatusCode = 200
            };
        }

    }
}
