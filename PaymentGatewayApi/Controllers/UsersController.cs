using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.DataLayer;
using PaymentGatewayApi.Models.DTOs;

namespace PaymentGatewayApi.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<Merchant> _userManager;
        private SignInManager<Merchant> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly MyDbContext _myDbContext;
        private readonly HttpContext _httpContext;



        public UsersController(UserManager<Merchant> userManager, SignInManager<Merchant> signInManager, IOptions<ApplicationSettings> appSettings, MyDbContext myDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _myDbContext = myDbContext;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        //POST: /api/Users/Register
        public async Task<Object> Register(MerchantDto model) //metoda post działa
        {
            var user = new Merchant()
            {
                UserName = model.UserName,
                Email = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user);
                await _userManager.AddPasswordAsync(user, model.Password);
                await _userManager.UpdateAsync(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        //POST: /api/Users/Login
        public async Task<IActionResult> Login(MerchantDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("login", user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }  
        }

        //[HttpDelete]
        //[Route("Delete")]
        //public async Task<Object> Delete(MerchantDto model)
        //{
        //    try
        //    {

        //        var user = await _userManager.FindByEmailAsync(model.Email);

        //        await _userManager.DeleteAsync(user);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}









    }

}
