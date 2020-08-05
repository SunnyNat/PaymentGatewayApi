using System.Security.Claims;
using System.Threading.Tasks;
using CreditCardValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentGatewayApi.DataLayer;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Models.DTOs;
using PaymentGatewayApi.Service;

namespace PaymentGatewayApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PaymentsController : ControllerBase
    {
        private UserManager<Merchant> _userManager;
        private PaymentUtils _paymentUtils;
        private readonly IOptions<ApplicationSettings> _options;

        public PaymentsController(UserManager<Merchant> userManager, IOptions<ApplicationSettings> options, IPaymentRepository repository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _options = options;
            _paymentUtils = new PaymentUtils(repository, unitOfWork);
        }

        [HttpPost]
        [Route("Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //POST: /api/Payments/Post
        public async Task<ActionResult<BankResponseDto>> Post([FromBody] PaymentRequestDto paymentRequestDto)
        {

            CardValidation cardValidation = new CardValidation(paymentRequestDto.Card);
            if (!cardValidation.IsValid)
            {
                return BadRequest(new { message = "Card data is incorrect." });
            }

            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst("login").Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            BankResponseDto bankResponseDto = await _paymentUtils.PostPayment(paymentRequestDto, user, _options.Value.BankUrl);

            if (bankResponseDto != null)
            {
                return Ok(bankResponseDto);
            }
            else
            {
                return NotFound(new { message = "Bank could not process the request" });
            }
        }

        [HttpGet]
        [Route("Get/{paymentIdentifier}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //GET: /api/Payments/Get/{paymentIdentifier}
        public async Task<ActionResult<PaymentDetailsDto>> Get([FromRoute] string paymentIdentifier)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst("login").Value;
            var user = await _userManager.FindByNameAsync(currentUserName);

            PaymentDetailsDto paymentDetailsDto = await _paymentUtils.GetPaymentDetails(paymentIdentifier, user);

            if (paymentDetailsDto != null)
            {
                return Ok(paymentDetailsDto);
            }
            else
            {
                return NotFound(new { message = "Payment with given identifier was not found." });
            }
        }

        [HttpGet]
        [Route("Get/RandomCardNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //GET: /api/Payments/Get/RandomCardNumber
        public ActionResult<string> GetRandomVisaNumber()
        {
            string visaNumber = CreditCardFactory.RandomCardNumber(CardIssuer.Visa);
            return Ok(new { visaNumber });
        }
    
    }
}
