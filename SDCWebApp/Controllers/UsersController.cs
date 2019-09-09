using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SDCWebApp.Auth;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Models.ViewModels;
using SDCWebApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomApiController//, IUsersController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IOptions<JwtSettings> _jwtOptions;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IRefreshTokenManager _refreshTokenMananger;
        private readonly IMapper _mapper;


        public UsersController(UserManager<IdentityUser> userManager, IJwtTokenHandler jwtTokenHandler, IRefreshTokenManager refreshTokenManager, IOptions<JwtSettings> options, ILogger<UsersController> logger, IMapper mapper) : base(logger)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _logger = logger;
            _userManager = userManager;
            _jwtOptions = options;
            _mapper = mapper;
            _refreshTokenMananger = refreshTokenManager;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel loginData)
        {

            Ticket _validTicket = new Ticket
            {
                Id = "1",
                Customer = new Customer { Id = "1", EmailAddress = "samplecustomer@mail.com" },
                Discount = new Discount { Id = "1", Description = "discount description", DiscountValueInPercentage = 25, Type = Discount.DiscountType.ForChild },
                Group = new SightseeingGroup { Id = "1", MaxGroupSize = 30, SightseeingDate = DateTime.Now.AddDays(1) },
                Tariff = new TicketTariff { Id = "1", DefaultPrice = 30, Description = "ticket price list description" },
                PurchaseDate = DateTime.Now.AddDays(-1),
                TicketUniqueId = Guid.NewGuid().ToString()
            };

            var dto = _mapper.Map<TicketDto>(_validTicket);



            _logger.LogInformation($"Starting method '{nameof(LoginAsync)}'.");

            var user = await _userManager.FindByNameAsync(loginData.UserName);

            if (!(user is null) && await _userManager.CheckPasswordAsync(user, loginData.Password))
            {
                try
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _jwtTokenHandler.CreateJwtToken(user, userRoles.ToArray());
                    string accessToken = _jwtTokenHandler.WriteJwtToken(jwtToken);
                    var newRefreshToken = _refreshTokenMananger.GenerateRefreshToken();

                    // Save refresh token to the database for future exachange.
                    await _refreshTokenMananger.AddRefreshTokenAsync(newRefreshToken);

                    var loginInfo = new LoginInfo
                    {
                        User = new UserDto { Id = user.Id, UserName = user.UserName, LoggedOn = DateTime.UtcNow },
                        AccessToken = new AccessToken(accessToken, (int)jwtToken.Payload.Exp),
                        RefreshToken = MapToDto(newRefreshToken)
                    };
                    var response = new ResponseWrapper(loginInfo);
                    _logger.LogInformation($"Finished method '{nameof(LoginAsync)}'.");
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    LogUnexpectedException(ex);
                    throw;
                }
            }
            else
            {
                // TODO Add better approach to failed login.
                var response = new ResponseWrapper();
                return Ok(response);
            }
        }

        //// [POST] users/logout
        //[HttpPost("logout")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public Task<IActionResult> LogoutAsync([FromBody] LogoutViewModel logoutData)
        // {
        //     throw new NotImplementedException();
        // }

        ////[POST] users/register
        //[HttpPost("register")]
        ////[Authorize(Policy = "OnlyForAdmin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerData)
        //{
        //    var user = new IdentityUser
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = registerData.UserName,
        //        Email = registerData.EmailAddress
        //    };

        //    var createdResult = await _userManager.CreateAsync(user, registerData.Password);

        //    if (createdResult.Succeeded)
        //    {
        //        await _userManager.AddToRoleAsync(user, "admin");

        //        // TODO Send confirmation email.

        //        var registerInfo = new
        //        {
        //            userName = registerData.UserName,
        //            id = user.Id
        //        };
        //        var response = new ResponseWrapper(registerInfo);
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return Ok(new ResponseWrapper());
        //    }
        //}


        #region Privates

        private RefreshTokenDto MapToDto(RefreshToken refreshToken) => _mapper.Map<RefreshTokenDto>(refreshToken);

        #endregion
    }
}