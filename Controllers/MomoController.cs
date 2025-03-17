using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBusniess;
using MusicBusniess.Data;
using MusicCenterAPI.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Claims;

namespace MusicCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private IMusicCenterAPI _api;
        public MomoController(IMusicCenterAPI api)
        {
            _api = api;
        }
        [Authorize]
        [HttpPost("create/{packName}")]
        public async Task<IActionResult> CreatePayment(string packName)
        {
            Console.WriteLine(DateTime.Now+ " | đăng ký gói premium "+packName);
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            token = token.Substring(7);
            var principal = _api.DecodeToken(token);

            AccountData account = new AccountData(_api, principal.FindFirst(ClaimTypes.Name)?.Value);

            long amount = 10000;   
            string packNameFinal = "";
            int exNewDate = 0;
            if (packName == "30DAY")
            {
                amount = 20000;
                exNewDate = 30;
                packNameFinal = "30 Ngày";
            }else if (packName == "1Y")
            {
                amount = 100000;
                exNewDate = 365;

                packNameFinal = "1 Năm";
            }else if (packName == "NOEX")
            {
                amount = 670000;
                exNewDate = -1;
                packNameFinal = "Vĩnh viễn";

            }
            string reqId = Guid.NewGuid().ToString();
            string odId = Guid.NewGuid().ToString();

            string accessKey = "F8BBA842ECF85";
            string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

            CollectionLinkRequest request = new CollectionLinkRequest(_api);

            request.orderInfo = "PremiumReg "+packName;
            request.partnerCode = "MOMO";
            request.redirectUrl = "";
            request.ipnUrl = "https://localhost:7165/Momo/api/call-back"; // đường dẫn api call-back, phía Momo sẽ gửi dữ liệu thanh toán về đây
            request.redirectUrl = "https://localhost:7089/pay_confirm/"+reqId;
            request.amount = amount;
            request.orderId = odId;
            request.requestId = reqId;
            request.requestType = "payWithMethod";
            request.extraData = "";
            request.partnerName = "MusicCenter";
            request.storeId = "Test Store";
            request.orderGroupId = "";
            request.autoCapture = true;
            request.active = false;
            request.lang = "vi";
            request.username = account.UserName;

            var rawSignature = "accessKey=" + accessKey + "&amount=" + request.amount + "&extraData=" + request.extraData + "&ipnUrl=" + request.ipnUrl + "&orderId=" + request.orderId + "&orderInfo=" + request.orderInfo + "&partnerCode=" + request.partnerCode + "&redirectUrl=" + request.redirectUrl + "&requestId=" + request.requestId + "&requestType=" + request.requestType;
            request.signature = MomoService.getSignature(rawSignature, secretKey);

            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var quickPayResponse = await MomoService.client.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);
            var contents = quickPayResponse.Content.ReadAsStringAsync().Result;
            request.Save();

            return Ok(contents);
        }
        [HttpPost("call-back")]
        public IActionResult CallBack([FromForm] CollectionLinkRequest model)
        {
            /* Đây là API nhận dữ liệu từ phía Momo, cần hosting url để phía Momo có thể gửi dữ liệu, nhưng
             * Đây là dự án chạy trên máy (localhost) nên momo không thể gửi, triển khai thực tế sẽ cần thay đổi đường
             * Dẫn của dự án để Momo có thể gửi dữ liệu, và dữ liệu sẽ gửi thông qua model
            */
            return Ok(model);
        }
    }
}
