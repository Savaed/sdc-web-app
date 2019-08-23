using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SDCWebApp.Controllers;
using SDCWebApp.Helpers;
using SDCWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests.Controllers
{
    public class UsersControllerTests
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private IOptions<JwtSettings> _options;
        private Mock<IMapper> _mapperMock;


        [OneTimeSetUp]
        public void SetUp()
        {
            _userManagerMock = GetUserManager();

            var jwtOptions = new JwtSettings
            {
                Audience = "https://www.google.com",
                ExpiryTime = "60",
                Issuer = "https://www.google.com",
                Secret = "dfg876f86sSDFsf87227424"
            };
            _options = Mock.Of<IOptions<JwtSettings>>(o => o.Value == jwtOptions);
            _mapperMock = new Mock<IMapper>();
           // _mapperMock.Setup(x => x.Map<UserDto>(It.IsAny<IdentityUser>())).Returns(new UserDto { Id = "1", Token = "token", UserName = "username" });
        }


        #region LoginAsync()    
        // zly login albo haslo -> 200 ok z errorem
        // znaleziono usera i haslo sie zgadza -> 200 ok, bez errorw
        // znaleziono usera i haslo sie zgadza -> 200 ok, z danymi usera (token, id)


        [Test]
        public async Task LoginAsync__User_not_found_or_password_is_incorrect__Should_return_200OK_with_error_and_without_data()
        {
            SetUpManagerForFailedLogin();
            var loginData = new LoginViewModel { Password = "passwordA1_", UserName = "username" };
            var usersController = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await usersController.LoginAsync(loginData);

            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
            ((result as OkObjectResult).Value as CommonWrapper).Errors.Should().NotBeEmpty();
            ((result as OkObjectResult).Value as CommonWrapper).Data.Should().BeNull();
        }

        [Test]
        public async Task LoginAsync__User_found__Should_return_200OK_without_error()
        {
            SetUpManagerForSuccessedLogin();
            var loginData = new LoginViewModel { Password = "passwordA1_", UserName = "username" };
            var usersController = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await usersController.LoginAsync(loginData);

            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
            ((result as OkObjectResult).Value as CommonWrapper).Errors.Should().BeNull();
        }

        [Test]
        public async Task LoginAsync__User_found__Should_return_200OK_with_user_data()
        {
            SetUpManagerForSuccessedLogin();
            var loginData = new LoginViewModel { Password = "passwordA1_", UserName = "username" };
            var usersController = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await usersController.LoginAsync(loginData);

            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
            ((result as OkObjectResult).Value as CommonWrapper).Data.Should().NotBeNull();
        }

        #endregion

        #region RegisterAsync()
        // nie ma takiej roli w bazie jak podana -> 200 ok, error
        // zajety login/haslo -> 200 ok, error
        // udalo sie dodac user -> 200 ok, brak bledow
        // jw -> 200 ok, zwraca dane usera (id, username)  

        [Test]
        public async Task RegisterAsync__Passed_role_doesnt_exist__Should_return_200OK_with_error()
        {
            SetUpManagerForFailedRegister();
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Users.RemoveRange(await context.Users.ToArrayAsync());
                    context.Roles.RemoveRange(await context.Roles.ToArrayAsync());
                    await context.SaveChangesAsync();
                }
            }
            var registerData = new RegisterViewModel { EmailAddress = "sample@mail.com", Password = "abcABC123_", UserName = "username", Role = "role" };
            var controller = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await controller.RegisterAsync(registerData);

            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
            ((result as OkObjectResult).Value as CommonWrapper).Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task RegisterAsync__Login_or_email_already_taken__Should_return_200OK_with_error()
        {
            SetUpManagerForFailedRegister();
            string role = "role";
            string email = "sample@mail.com";
            string userName = "username";
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Users.Add(new IdentityUser
                    {
                        Email = email,
                        UserName = userName
                    });
                    context.Roles.Add(new IdentityRole(role));
                    await context.SaveChangesAsync();
                }
            }
            var registerData = new RegisterViewModel { EmailAddress = email, Password = "abcABC123_", UserName = userName, Role = role };
            var controller = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await controller.RegisterAsync(registerData);

            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be(200);
            ((result as OkObjectResult).Value as CommonWrapper).Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task RegisterAsync__Register_successful__Should_return_201Created_without_error()
        {
            SetUpManagerForSuccessedRegister();
            string role = "role";
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Users.RemoveRange(await context.Users.ToArrayAsync());
                    context.Roles.Add(new IdentityRole(role));
                    await context.SaveChangesAsync();
                }
            }
            var registerData = new RegisterViewModel { EmailAddress = "sample@mail.com", Password = "abcABC123_", UserName = "username", Role = role};
            var controller = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await controller.RegisterAsync(registerData);

            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).StatusCode.Should().Be(201);
            ((result as CreatedResult).Value as CommonWrapper).Errors.Should().BeNull();
        }

        [Test]
        public async Task RegisterAsync__Register_successful__Should_return_201Created_with_user_data()
        {
            SetUpManagerForSuccessedRegister();
            string role = "role";
            using (var factory = new DbContextFactory())
            {
                using (var context = await factory.CreateContextAsync())
                {
                    context.Users.RemoveRange(await context.Users.ToArrayAsync());
                    context.Roles.Add(new IdentityRole(role));
                    await context.SaveChangesAsync();
                }
            }
            var registerData = new RegisterViewModel { EmailAddress = "sample@mail.com", Password = "abcABC123_", UserName = "username", Role = role };
            var controller = new UsersController(_userManagerMock.Object, _options, _mapperMock.Object);

            var result = await controller.RegisterAsync(registerData);

            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).StatusCode.Should().Be(201);
            //((result as CreatedResult).Value as CommonWrapper).Data.Should().BeOfType<UserDto>();
            ((result as CreatedResult).Value as CommonWrapper).Data.Should().NotBeNull();
        }

        #endregion

        #region Privates

        private Mock<UserManager<IdentityUser>> GetUserManager()
        {
            var userPasswordStore = Mock.Of<IUserPasswordStore<IdentityUser>>();
            var identityOptions = new IdentityOptions
            {
                // Options should be the same as in Startup class. In the other case it won't be representing actual valid parameters.
                Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true
                },
                Lockout = new LockoutOptions
                {
                    AllowedForNewUsers = true,
                    MaxFailedAccessAttempts = 3
                },
                SignIn = new SignInOptions { RequireConfirmedEmail = false },
                User = new UserOptions
                {
                    RequireUniqueEmail = true,
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz"
                }
            };
            var options = Mock.Of<IOptions<IdentityOptions>>(o => o.Value == identityOptions);
            var userValidators = new List<IUserValidator<IdentityUser>> { Mock.Of<IUserValidator<IdentityUser>>() };
            var passwordValidators = new List<IPasswordValidator<IdentityUser>> { Mock.Of<IPasswordValidator<IdentityUser>>() };
            var passworHasher = Mock.Of<IPasswordHasher<IdentityUser>>();
            var upperInvariantLookupNormalizer = Mock.Of<ILookupNormalizer>();
            var identityErrorDescriber = Mock.Of<IdentityErrorDescriber>();
            var serviceProvider = Mock.Of<IServiceProvider>();
            var logger = Mock.Of<ILogger<UserManager<IdentityUser>>>();

            var userManager = new Mock<UserManager<IdentityUser>>(userPasswordStore,
                                                                  options,
                                                                  passworHasher,
                                                                  userValidators,
                                                                  passwordValidators,
                                                                  upperInvariantLookupNormalizer,
                                                                  identityErrorDescriber,
                                                                  serviceProvider,
                                                                  logger);
            return userManager;
        }

        private void SetUpManagerForSuccessedRegister()
        {
            _userManagerMock.Setup(m => m.CreateAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(m => m.AddToRoleAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>())).ReturnsAsync(IdentityResult.Success);
        }

        private void SetUpManagerForFailedRegister()
        {
            _userManagerMock.Setup(m => m.CreateAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Code = "code", Description = "desc" }))).Verifiable();
            _userManagerMock.Setup(m => m.AddToRoleAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>())).ReturnsAsync(IdentityResult.Failed());
        }

        private void SetUpManagerForSuccessedLogin()
        {
            _userManagerMock.Setup(m => m.FindByNameAsync(It.IsNotNull<string>())).ReturnsAsync(new IdentityUser("NewUser"));
            _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsNotNull<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(true);

            var roles = new List<string>().Append("Administrator").Append("Moderator");
            _userManagerMock.Setup(m => m.GetRolesAsync(It.IsNotNull<IdentityUser>())).ReturnsAsync(new string[] { "admin" } as IList<string>);
        }

        private void SetUpManagerForFailedLogin()
        {
            _userManagerMock.Setup(m => m.FindByNameAsync(It.IsNotNull<string>())).ReturnsAsync(null as IdentityUser);
            _userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsNotNull<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(false);
        }

        #endregion

    }
}
