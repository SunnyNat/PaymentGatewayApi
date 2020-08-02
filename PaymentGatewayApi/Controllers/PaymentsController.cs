using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using PaymentGatewayApi.Service;

namespace PaymentGatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private UserManager<Merchant> _userManager;
        private SignInManager<Merchant> _signInManager;
        private PaymentUtils _paymentUtils;
        private readonly IOptions<ApplicationSettings> _options;

        public PaymentsController(UserManager<Merchant> userManager, SignInManager<Merchant> signInManager, IOptions<ApplicationSettings> options, PaymentUtils paymentUtils)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options;
            _paymentUtils = paymentUtils;

        }

        [HttpPost]
        [Route("Post")]
        //POST: /api/Payments/Post
        public async Task<Object> Post([FromBody] PaymentDetailsDto paymentDetailsDto)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            BankResponseDto bankResponseDto = _paymentUtils.PostPayment(paymentDetailsDto, currentUser, _options.Value.BankUrl);

            if (bankResponseDto != null)
            {
                return Ok(bankResponseDto);
            }
            else
            {
                return new NotFoundObjectResult(bankResponseDto);
            }
        }

        [HttpGet]
        [Route("Get/{paymentIdentifier}")]
        //GET: /api/Payments/Get/{paymentIdentifier}
        public ObjectResult Get([FromRoute] string paymentIdentifier) //metoda post działa
        {
            PaymentDetailsDto paymentDetailsDto = _paymentUtils.GetPaymentDetails(paymentIdentifier);

            if(paymentDetailsDto != null)
            {
                return Ok(paymentDetailsDto);
            }
            else
            {
                return new NotFoundObjectResult(paymentDetailsDto);
            }
        }
    }
}
