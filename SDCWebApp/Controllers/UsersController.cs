//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Autofac.Features.Indexed;
//using Microsoft.AspNetCore.Mvc.Core;
//using FluentValidation;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using SDCWebApp.Data.Validators;
//using SDCWebApp.Models.ViewModels;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.Extensions.Options;
//using SDCWebApp.Helpers;
//using AutoMapper;

//namespace SDCWebApp.Controllers
//{
//    // TODO Add 500 Internal Server Error when db doesnt exist and any request that reffered to db can be fullfiled

//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase, IUsersController
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly IIndex<string, IValidator> _validatorsFactory;
//        private readonly ILogger<UsersController> _logger;
//        private readonly IOptions<JwtSettings> _jwtOptions;
//        private readonly IMapper _mapper;


//        public UsersController(UserManager<IdentityUser> userManager, IOptions<JwtSettings> options, IMapper mapper)
//        {
//            _userManager = userManager;
//            _jwtOptions = options;
//            _mapper = mapper;
//        }

//        //public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IIndex<string, IValidator> validatorsFactory, ILogger<UsersController> logger)
//        //{
//        //    _userManager = userManager;
//        //    _signInManager = signInManager;
//        //    _validatorsFactory = validatorsFactory;
//        //    _logger = logger;
//        //}


//        // [POST] users/login
//        [HttpPost("login")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel loginData)
//        {

//            throw new NotImplementedException();

//            //var tempUser = await _userManager.FindByNameAsync(loginData.UserName);

//            //if (tempUser is null || await _userManager.CheckPasswordAsync(tempUser, loginData.Password) == false)
//            //{
//            //    var errorResponse = new CommonWrapper
//            //    {
//            //        Success = false,
//            //        Errors = new List<ApiError>
//            //        {
//            //            new ApiError
//            //            {
//            //                ErrorCode = "IncorrectCredentials",
//            //                ErrorMessage = "Incorrect login or password."
//            //            }
//            //        }
//            //    };
//            //    return Ok(errorResponse);
//            //}
//            //else
//            //{
//            //    var tokenHandler = new JwtSecurityTokenHandler();
//            //    var tokenDescriptor = await CreateTokenDescriptorAsync(tempUser);
//            //    var token = tokenHandler.CreateToken(tokenDescriptor);

//                //var user = _mapper.Map<UserDto>(tempUser);
//                //user.Token = tokenHandler.WriteToken(token);
//                //var response = new CommonWrapper
//                //{
//                //    Success = true,
//                //    Data = user
//                //};
//                //return Ok(response);
//            //}
//        }

//        // [POST] users/logout
//        [HttpPost("logout")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public Task<IActionResult> LogoutAsync([FromBody] LogoutViewModel logoutData)
//        {
//            throw new NotImplementedException();
//        }

//        // [POST] users/register
//        [HttpPost("register")]
//        [Authorize(Policy = "OnlyForAdmin")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status403Forbidden)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel registerData)
//        {

//            throw new NotImplementedException();

//            //var newUser = new IdentityUser
//            //{
//            //    Email = registerData.EmailAddress,
//            //    UserName = registerData.UserName
//            //};

//            //var createUserResult = await _userManager.CreateAsync(newUser, registerData.Password);

//            //if (createUserResult.Succeeded)
//            //{
//            //    // TODO Sending email confirmation.

//            //    await _userManager.AddToRoleAsync(newUser, registerData.Role);
//            //    string url = $"https://localhost:44377/api/users/{ await _userManager.GetUserIdAsync(newUser)}";

//            //    var response = new CommonWrapper
//            //    {
//            //        Success = true,
//            //        Data = _mapper.Map<UserDto>(newUser)
//            //    };
//            //    return Created(url, response);
//            //}
//            //else
//            //{
//            //    List<ApiError> errors = new List<ApiError>();

//            //    foreach (var error in createUserResult.Errors)
//            //    {
//            //        errors.Add(new ApiError
//            //        {
//            //            ErrorCode = error.Code,
//            //            ErrorMessage = error.Description
//            //        });
//            //    }

//            //    var errorResponse = new CommonWrapper
//            //    {
//            //        Success = false,
//            //        Errors = errors
//            //    };
//            //    return Ok(errorResponse);
//            //}
//        }


//        #region Privates

//        private async Task<SecurityTokenDescriptor> CreateTokenDescriptorAsync(IdentityUser user)
//        {
//            var roles = await _userManager.GetRolesAsync(user);
//            byte[] signingKey = Encoding.ASCII.GetBytes(_jwtOptions.Value.Secret);

//            return new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                        {
//                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                        new Claim(ClaimTypes.NameIdentifier, user.Id),
//                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
//                        new Claim("loggedOn", DateTime.Now.ToString())
//                        }),
//                Audience = _jwtOptions.Value.Audience,
//                IssuedAt = DateTime.Now,
//                Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtOptions.Value.ExpiryTime)),
//                Issuer = _jwtOptions.Value.Issuer,
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
//            };
//        }

//        #endregion
//    }
//}