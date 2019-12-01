using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.ApiErrors;
using SDCWebApp.Auth;
using SDCWebApp.Helpers.Constants;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Models.ViewModels;
using SDCWebApp.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Supports registration, login and logout operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomApiController, IUsersController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IRefreshTokenManager _refreshTokenMananger;
        private readonly IMapper _mapper;


        public UsersController(UserManager<IdentityUser> userManager, IJwtTokenHandler jwtTokenHandler, IRefreshTokenManager refreshTokenManager, ILogger<UsersController> logger, IMapper mapper) : base(logger)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _refreshTokenMananger = refreshTokenManager;
        }


        /// <summary>
        /// Asynchronously logs a registered user. Returns <see cref="HttpStatusCode.BadRequest"/> if request is malformed, otherwise <see cref="HttpStatusCode.OK"/>.
        /// </summary>
        /// <param name="loginData">User data passed during login by the client. It is a JSON request body. Cannot be null.</param>
        /// <returns>Loged user datailed info.</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel loginData)
        {
            _logger.LogInformation($"Starting method '{nameof(LoginAsync)}'.");

            var user = await _userManager.FindByNameAsync(loginData.UserName);

            if (!(user is null) && await _userManager.CheckPasswordAsync(user, loginData.Password))
            {
                try
                {
                    _logger.LogDebug("User found.");
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

                    _jwtTokenHandler.GetPrincipalFromJwtToken(accessToken);

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
                var response = new ResponseWrapper(new LoginError("Invalid username or password."));
                _logger.LogInformation($"Finished method '{nameof(LoginAsync)}'.");
                return Ok(response);
            }
        }      

        /// <summary>
        /// Asynchronously register a new user. Returns <see cref="HttpStatusCode.BadRequest"/> if request is malformed 
        /// and <see cref="HttpStatusCode.Created"/> if registration succeeded. Otherwise returns <see cref="HttpStatusCode.OK"/>.
        /// </summary>
        /// <param name="registerData">User data passed during registration by the client. It is a JSON request body. Cannot be null.</param>
        /// <returns>New registered user datailed info.</returns>
        [HttpPost("register")]
        [Authorize(ApiConstants.ApiAdminPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerData)
        {
            _logger.LogInformation($"Starting method '{nameof(RegisterAsync)}'.");

            var user = CreateUser(registerData);
            var createdResult = await _userManager.CreateAsync(user, registerData.Password);

            if (createdResult.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, registerData.Role);

                if (addToRoleResult.Succeeded)
                {
                    _logger.LogDebug("User register succeeded.");

                    // TODO: Send confirmation email.

                    var registerInfo = new UserDto { Id = user.Id, UserName = user.UserName, LoggedOn = DateTime.MinValue };
                    string userUrl = $"users/{user.Id}";
                    var response = new ResponseWrapper(registerInfo);
                    _logger.LogInformation($"Finished method '{nameof(RegisterAsync)}'.");
                    return Created(userUrl, response);
                }
                else
                {
                    return Ok(new ResponseWrapper(new RegistrationError($"Registration failed. {addToRoleResult.Errors.ToList().First().Description}")));
                }
            }
            else
            {
                return Ok(new ResponseWrapper(new RegistrationError($"Registration failed. {createdResult.Errors.First().Description}")));
            }
        }


        #region Privates

        private IdentityUser CreateUser(RegisterViewModel registerData)
        {
            return new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerData.UserName,
                Email = registerData.EmailAddress
            };
        }

        private RefreshTokenDto MapToDto(RefreshToken refreshToken) => _mapper.Map<RefreshTokenDto>(refreshToken);

        #endregion

    }
}