using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.PaymentService.Application.Interfaces;

namespace ShopSphere.PaymentService.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
      
        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentRequestDto request)
        {
            var result =
                await _paymentService
                    .CreatePaymentAsync(request);

            return Ok(result);
        }
    }
}
