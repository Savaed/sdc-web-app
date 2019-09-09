using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SDCWebApp.ApiErrors;
using SDCWebApp.Controllers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class DiscountsControllerTests
    {
        private Mock<IDiscountDbService> _discountDbServiceMock;
        private Mock<IMapper> _mapperMock;
        private ILogger<DiscountsController> _logger;
        private Discount _validDiscount;
        private Discount[] _discounts;
        private DiscountDto[] _discountDtos;


        [OneTimeSetUp]
        public void SetUp()
        {
            _discountDbServiceMock = new Mock<IDiscountDbService>();
            _mapperMock = new Mock<IMapper>();
            _logger = Mock.Of<ILogger<DiscountsController>>();

            _validDiscount = new Discount
            {
                Id = "1",
                Description = "Sample test description",
                Type = Discount.DiscountType.ForChild,
                GroupSizeForDiscount = 0,
                DiscountValueInPercentage = 23,
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            };

            _discountDtos = new DiscountDto[]
            {
                 new DiscountDto
                 {
                    Id = "1",
                    Description = "This is DTO",
                    Type = Discount.DiscountType.ForChild,
                    GroupSizeForDiscount = 0,
                    DiscountValueInPercentage = 23,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 },
                 new DiscountDto
                 {
                    Id = "2",
                    Description = "This id DTO",
                    Type = Discount.DiscountType.ForChild,
                    GroupSizeForDiscount = 0,
                    DiscountValueInPercentage = 23,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 }
            };

            _discounts = new Discount[]
            {
                 new Discount
                 {
                    Id = "1",
                    Description = "This is DTO",
                    Type = Discount.DiscountType.ForChild,
                    GroupSizeForDiscount = 0,
                    DiscountValueInPercentage = 23,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 },
                 new Discount
                 {
                    Id = "2",
                    Description = "This id DTO",
                    Type = Discount.DiscountType.ForChild,
                    GroupSizeForDiscount = 0,
                    DiscountValueInPercentage = 23,
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                 }
            };
        }


        #region GetDiscountAsync(string id);
        // brak elementu o podanym id/pusty zasob -> 404 not found
        // string jest nullem, pusty -> 400 bad request, 'id' nie moze byc nullem lub pusty
        // znaleziono -> 200 ok
        // jakikolwiek error -> response zawiera error
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetDiscountAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _discountDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetDiscountAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetDiscountAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _discountDbServiceMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetDiscountAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetDiscountAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            _discountDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ThrowsAsync(new InvalidOperationException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetDiscountAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetDiscountAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id) 
        {
            _discountDbServiceMock.Setup(x => x.GetAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetDiscountAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task GetDiscountAsync__Data_retrieve_succeeded__Should_return_200Ok_response_with_data()
        {
            string id = "15891fb0-faec-43c6-9e83-04a4a17c3660";
            _discountDbServiceMock.Setup(x => x.GetAsync(It.IsNotNull<string>())).ReturnsAsync(_validDiscount);
            _mapperMock.Setup(x => x.Map<DiscountDto>(It.IsNotNull<Discount>())).Returns(new DiscountDto { Id = id, Description = "new mapped discount" });
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetDiscountAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region GetAllDiscountsAsync();
        // pusty zasob -> 200 ok, pusta lista
        // znalazlo min 1 element -> 200 ok i niepusta lista
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task GetAllDiscountAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _discountDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new InternalDbServiceException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllDiscountsAsync();

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task GetAllDiscountAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _discountDbServiceMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.GetAllDiscountsAsync();

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task GetAllDiscountAsync__Resource_is_empty__Should_return_200OK_response_with_empty_IEnumerable()
        {
            _discountDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Discount>());
            _mapperMock.Setup(x => x.Map<IEnumerable<DiscountDto>>(It.IsNotNull<IEnumerable<Discount>>())).Returns(Enumerable.Empty<DiscountDto>());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllDiscountsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<DiscountDto>).Should().BeEmpty();
        }

        [Test]
        public async Task GetAllDiscountAsync__At_least_one_element_found__Should_return_200OK_response_with_not_empty_IEnumerable()
        {
            _discountDbServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(_discounts);
            _mapperMock.Setup(x => x.Map<IEnumerable<DiscountDto>>(It.IsNotNull<IEnumerable<Discount>>())).Returns(_discountDtos.AsEnumerable());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.GetAllDiscountsAsync();

            (result as ObjectResult).StatusCode.Should().Be(200);
            (((result as ObjectResult).Value as ResponseWrapper).Data as IEnumerable<DiscountDto>).Should().NotBeEmpty();
        }

        #endregion


        #region AddDiscountAsync(Discount discount);
        // istnieje juz taki sam cennik w bazie -> 400 bad request instance already exists, details 'objname', 'id'
        // discount jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // jakies dane cenniku sa niepoprawne -> 400 bad request i info co jest nie tak
        // dodano poprawnie -> 201 created, zwraca dodany element
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task AddDiscountAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _discountDbServiceMock.Setup(x => x.AddAsync(It.IsAny<Discount>())).ThrowsAsync(new InternalDbServiceException());
            _discountDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<Discount>())).ThrowsAsync(new InternalDbServiceException());
            var discount = new DiscountDto { Id = "1", Description = "test" };
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.AddDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task AddDiscountAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _discountDbServiceMock.Setup(x => x.AddAsync(It.IsAny<Discount>())).ThrowsAsync(new Exception());
            _discountDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsAny<Discount>())).ThrowsAsync(new Exception());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);
            var discount = new DiscountDto { Id = "1", Description = "test" };

            Func<Task> result = async () => await controller.AddDiscountAsync(discount);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task AddDiscountAsync__Already_there_is_the_same_element_in_database__Should_return_400BadRequest_response()
        {
            var discountDto = CreateDiscountDto(_discounts[0]);
            _mapperMock.Setup(x => x.Map<Discount>(discountDto)).Returns(_discounts[0]);
            _discountDbServiceMock.Setup(x => x.AddAsync(It.IsNotNull<Discount>())).ThrowsAsync(new InvalidOperationException());
            _discountDbServiceMock.Setup(x => x.RestrictedAddAsync(It.IsNotNull<Discount>())).ThrowsAsync(new InvalidOperationException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddDiscountAsync(discountDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task AddDiscountAsync__Add_succeeded__Should_return_200OK_response_with_added_element()
        {
            var validDiscount = new Discount { Id = "12312321321321", Description = "Valid description" };
            var validDiscountDto = CreateDiscountDto(validDiscount);
            _mapperMock.Setup(x => x.Map<Discount>(It.IsNotNull<DiscountDto>())).Returns(validDiscount);
            _mapperMock.Setup(x => x.Map<DiscountDto>(It.IsNotNull<Discount>())).Returns(validDiscountDto);
            _discountDbServiceMock.Setup(x => x.AddAsync(validDiscount)).ReturnsAsync(validDiscount);
            _discountDbServiceMock.Setup(x => x.RestrictedAddAsync(validDiscount)).ReturnsAsync(validDiscount);
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.AddDiscountAsync(validDiscountDto);

            (result as ObjectResult).StatusCode.Should().Be(201);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().NotBeNull();
        }

        #endregion


        #region UpdateDiscountAsync(string id, Discount discount);
        // nie ma takiego elementu o podanym id -> 404 not found
        // id w url i id w body sie nie zgadzaja -> 400 bad request, mismatch
        // id / discount jest nullem -> 400 bad request, arg nie moze byc nullem, id nie moze byc puste
        // update poprawny -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task UpdateDiscountAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _discountDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Discount>())).ThrowsAsync(new InternalDbServiceException());
            _discountDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<Discount>())).ThrowsAsync(new InternalDbServiceException());
            _mapperMock.Setup(x => x.Map<Discount>(It.IsNotNull<Discount>())).Returns(_validDiscount);
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateDiscountAsync(_discountDtos[0].Id, _discountDtos[0]);

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task UpdateDiscountAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _discountDbServiceMock.Setup(x => x.UpdateAsync(It.IsAny<Discount>())).ThrowsAsync(new Exception());
            _discountDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsAny<Discount>())).ThrowsAsync(new Exception());
            _mapperMock.Setup(x => x.Map<Discount>(It.IsNotNull<Discount>())).Returns(_validDiscount);
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.UpdateDiscountAsync(_discountDtos[0].Id, _discountDtos[0]);

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task UpdateDiscountAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            var validDiscount = new Discount { Id = "12312321321321", Description = "Valid description" };
            var validDiscountDto = CreateDiscountDto(validDiscount);
            _mapperMock.Setup(x => x.Map<Discount>(It.IsNotNull<DiscountDto>())).Returns(validDiscount);
            _mapperMock.Setup(x => x.Map<DiscountDto>(It.IsNotNull<Discount>())).Returns(validDiscountDto);
            _discountDbServiceMock.Setup(x => x.UpdateAsync(It.IsNotNull<Discount>())).ThrowsAsync(new InvalidOperationException());
            _discountDbServiceMock.Setup(x => x.RestrictedUpdateAsync(It.IsNotNull<Discount>())).ThrowsAsync(new InvalidOperationException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);


            var result = await controller.UpdateDiscountAsync(validDiscountDto.Id, validDiscountDto);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateDiscountAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            var discountDto = new DiscountDto { Id = id, Description = "Test dto description" };
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateDiscountAsync(id, discountDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateDiscountAsync__Argument_id_and_id_property_in_element_to_be_updated_mismatches__Should_return_400BadRequest_response()
        {
            var discountDto = new DiscountDto { Id = "1", Description = "Test dto description" };
            string id = discountDto.Id + "_mismatched_id";
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateDiscountAsync(id, discountDto);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task UpdateDiscountAsync__Update_succeeded__Should_return_200OK_response_with_updated_element()
        {
            var validDiscount = new Discount { Id = "12312321321321", Description = "Valid description" };
            var validDiscountDto = CreateDiscountDto(validDiscount);
            _mapperMock.Setup(x => x.Map<Discount>(It.IsNotNull<DiscountDto>())).Returns(validDiscount);
            _mapperMock.Setup(x => x.Map<DiscountDto>(It.IsNotNull<Discount>())).Returns(validDiscountDto);
            _discountDbServiceMock.Setup(x => x.UpdateAsync(validDiscount)).ReturnsAsync(validDiscount);
            _discountDbServiceMock.Setup(x => x.RestrictedUpdateAsync(validDiscount)).ReturnsAsync(validDiscount);
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.UpdateDiscountAsync(validDiscountDto.Id, validDiscountDto);

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Data.Should().BeEquivalentTo(validDiscountDto);
        }

        #endregion


        #region DeleteDiscountAsync(string id);
        // nie ma takiego elementu o podanym id -> 404 not found
        // argument jest pusty albo null -> 400 bad request
        // usunieto poprawnie -> 200 ok
        // any internal error refferd to the db occurred -> throws internal db service exc
        // any unexpected internal error occurred -> throws exc

        [Test]
        public async Task DeleteDiscountAsync__An_internal_error_reffered_to_the_database_occurred__Should_throw_InternalDbServiceException()
        {
            // Example of these errors: database does not exist, table does not exist etc.

            _discountDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new InternalDbServiceException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteDiscountAsync("1");

            await result.Should().ThrowExactlyAsync<InternalDbServiceException>();
        }

        [Test]
        public async Task DeleteDiscountAsync__An_unexpected_internal_error_occurred__Should_throw_Exception()
        {
            _discountDbServiceMock.Setup(x => x.DeleteAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            Func<Task> result = async () => await controller.DeleteDiscountAsync("1");

            await result.Should().ThrowExactlyAsync<Exception>();
        }

        [Test]
        public async Task DeleteDiscountAsync__Element_not_found__Should_return_404NotFound_response_with_error()
        {
            string id = "-1";
            _discountDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new InvalidOperationException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteDiscountAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(404);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteDiscountAsync__Argument_id_is_null_or_empty__Should_return_400BadRequest_response([Values(null, "")] string id)
        {
            _discountDbServiceMock.Setup(x => x.DeleteAsync(id)).ThrowsAsync(new ArgumentException());
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteDiscountAsync(id);

            (result as ObjectResult).StatusCode.Should().Be(400);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteDiscountAsync__Delete_succeeded__Should_return_200OK_response()
        {
            _discountDbServiceMock.Setup(x => x.DeleteAsync(It.IsNotNull<string>()));
            var controller = new DiscountsController(_discountDbServiceMock.Object, _logger, _mapperMock.Object);

            var result = await controller.DeleteDiscountAsync("1");

            (result as ObjectResult).StatusCode.Should().Be(200);
            ((result as ObjectResult).Value as ResponseWrapper).Error.Should().BeEquivalentTo(new ApiError());
        }

        #endregion


        #region Privates

        private DiscountDto CreateDiscountDto(Discount discount)
        {
            return new DiscountDto
            {
                Id = discount.Id,
                Description = discount.Description,
                DiscountValueInPercentage = discount.DiscountValueInPercentage,
                GroupSizeForDiscount = discount.GroupSizeForDiscount,
                Type = discount.Type
            };
        }

        #endregion
    }
}
