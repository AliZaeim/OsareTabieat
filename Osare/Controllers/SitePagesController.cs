using Core.DTOs.General;
using Core.Services.Interfaces;
using DataLayer.Entities.Store;
using DataLayer.Entities.Supplementary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Web.Controllers
{
    public class SitePagesController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStoreService _storeService;

        public SitePagesController(IHttpContextAccessor httpContextAccessor, IStoreService storeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _storeService = storeService;

        }
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("GoToPayment")]
        public async Task<IActionResult> GoToPaymentAsync(Guid? cartId, string BackUrl)
        {
            if (cartId == null)
            {
                return NotFound("امکان پرداخت برای سبد خرید وجود ندارد !");
            }
            Cart cart = await _storeService.GetCartByIdAsync(cartId.Value);
            string PayCur = "IRR";
            SiteInfo? siteInfo = await _storeService.GetLastSiteInfo();
            if (siteInfo != null)
            {
                if (siteInfo.SiteCurrency== "تومان")
                {
                    PayCur = "IRT";
                }
            }
            if (cart != null)
            {
                if (!cart.CheckOut)
                {
                    
                    (bool Success, string Content) = _storeService.GetNextPayToken(cart.CartTotal.GetValueOrDefault(), cart.Id.ToString(), cart.BuyerCellphone!, BackUrl, PayCur);
                    //_genericInsService.GetNextPayToken(Res.Amount.Value, Res.TrCode, Res.InsurerCellphone, BackUrl, InsType, InsId.ToString(), Currency);
                    if (Success)
                    {
                        string json = Content;
                        dynamic data = JObject.Parse(json);
                        string tid = data["trans_id"];
                        string eUrl = "https://nextpay.org/nx/gateway/payment/" + tid;
                        return Redirect(eUrl);
                    }
                }
            }

            return Content("انتقال به درگاه پرداخت");
            
            
            
        }
        
        [Route("PaymentResult")]
        public async Task<IActionResult> PaymentResult()
        {
            string? np_status = HttpContext.Request.Query["np_status"];
            string? amount = HttpContext.Request.Query["amount"];
            string? orderid = HttpContext.Request.Query["order_id"];
            string? transid = HttpContext.Request.Query["trans_id"];
            Guid CartId = Guid.Parse(orderid!);
            if (np_status == "OK")
            {
                Core.Utility.CookieExtensions.RemoveCookie("crt");
                string? url = "https://nextpay.org/nx/gateway/verify/";
                RestClient client = new(url);
                RestRequest request = new(url, Method.Post);
                _ = request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                _ = request.AddParameter("api_key", "47364478-b6ab-424a-b36f-c5b06c814120");
                _ = request.AddParameter("amount", amount);
                _ = request.AddParameter("trans_id", transid);
                _ = client.Execute(request);
                Cart cart = await _storeService.GetCartByIdAsync(CartId);
                
                List<string?> DedNumbers =await _storeService.GetCartsDedicatedNumbers();
                string dedN = Core.Prodocers.Generators.GenerateUniqueString(DedNumbers,0,0,8,0);
                if (cart != null)
                {
                    cart.CheckOut = true;
                    cart.PaymentDate= DateTime.Now;
                    cart.Comment += "شماره تراکنش : " + transid;  
                    cart.DedicatedCode = dedN;
                    _storeService.UpdateCart(cart);
                    _ = _storeService.UpdateWareHouseWithCart(CartId);
                    await _storeService.SaveChangesAsync();          

                }
                PaymentResultVM paymentResultVM = new()
                {
                    PayMessage = "پرداخت با موفقیت انجام شد و سفارش شما ثبت شد",
                    DedicatedNumber = dedN,
                    PayValue = amount!,
                    TransId = transid!,
                    OrderId = orderid!
                };
                return View(paymentResultVM);
            }
            else
            {
                return NotFound("پرداخت با موفقیت انجام نشد !");
            }
            
            
        }
        
    }
}
