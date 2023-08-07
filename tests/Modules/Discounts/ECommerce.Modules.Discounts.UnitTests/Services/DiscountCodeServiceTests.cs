using ECommerce.Modules.Discounts.Core.DTO;
using ECommerce.Modules.Discounts.Core.Entities;
using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Exceptions;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Modules.Discounts.Core.Services;
using ECommerce.Modules.Discounts.Core.Validators;
using ECommerce.Modules.Discounts.UnitTests.Time;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;
using FluentAssertions;
using Moq;

namespace ECommerce.Modules.Discounts.UnitTests.Services;

public class DiscountCodeServiceTests
{
    [Fact]
    public async Task AddAsync_GivenValidDiscount_ShouldAddDiscountCodeSuccessfully()
    {
        //Arrange
        var dto = new DiscountCodeDto
        {
            Id = Guid.NewGuid(),
            Code = "TEST33",
            Description = "test code",
            Percentage = 33,
            ProductIds = new List<Guid> {Guid.NewGuid()},
            ValidFrom = _clock.CurrentDate().AddHours(2), 
            ValidTo = _clock.CurrentDate().AddDays(2).AddHours(2)
        };

        var discountCodeRepositoryMock = new Mock<IDiscountCodeRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();

        productRepositoryMock
            .Setup(x => x.GetAsync(dto.ProductIds))
            .ReturnsAsync(new List<Product> {new Product {Id = dto.ProductIds.First()}});
        
        discountCodeRepositoryMock
            .Setup(x => x.ExistsAsync(dto.Code))
            .ReturnsAsync(false);

        var discountCodeService = new DiscountCodeService(discountCodeRepositoryMock.Object, productRepositoryMock.Object,
            _dateValidator, messageBrokerMock.Object);

        //Act
        await discountCodeService.AddAsync(dto);
        
        //Assert
        productRepositoryMock.Verify(x => x.GetAsync(dto.ProductIds), Times.Once);
        discountCodeRepositoryMock.Verify(x => x.ExistsAsync(dto.Code), Times.Once);
        discountCodeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<DiscountCode>()), Times.Once);
    }
    
    [Fact]
    public async Task AddAsync_GivenOneOrProductInvalid_ShouldThrowProductsNotFoundException()
    {
        //Arrange
        var dto = new DiscountCodeDto
        {
            Id = Guid.NewGuid(),
            Code = "TEST33",
            Description = "test code",
            Percentage = 33,
            ProductIds = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()},
            ValidFrom = _clock.CurrentDate().AddHours(2), 
            ValidTo = _clock.CurrentDate().AddDays(2).AddHours(2)
        };

        var discountCodeRepositoryMock = new Mock<IDiscountCodeRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();

        productRepositoryMock
            .Setup(x => x.GetAsync(dto.ProductIds))
            .ReturnsAsync(new List<Product> {new Product {Id = dto.ProductIds.First()}});

        var discountCodeService = new DiscountCodeService(discountCodeRepositoryMock.Object, productRepositoryMock.Object,
            _dateValidator, messageBrokerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => discountCodeService.AddAsync(dto));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ProductsNotFoundException>();
    }
    
    [Fact]
    public async Task AddAsync_GivenInvalidDiscountDate_ShouldThrowInvalidDiscountDateException()
    {
        //Arrange
        var dto = new DiscountCodeDto
        {
            Id = Guid.NewGuid(),
            Code = "TEST33",
            Description = "test code",
            Percentage = 33,
            ProductIds = new List<Guid> {Guid.NewGuid()},
            ValidFrom = _clock.CurrentDate().AddHours(-2), 
            ValidTo = _clock.CurrentDate().AddDays(2).AddHours(2)
        };

        var discountCodeRepositoryMock = new Mock<IDiscountCodeRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();

        productRepositoryMock
            .Setup(x => x.GetAsync(dto.ProductIds))
            .ReturnsAsync(new List<Product> {new Product {Id = dto.ProductIds.First()}});
        
        discountCodeRepositoryMock
            .Setup(x => x.ExistsAsync(dto.Code))
            .ReturnsAsync(false);

        var discountCodeService = new DiscountCodeService(discountCodeRepositoryMock.Object, productRepositoryMock.Object,
            _dateValidator, messageBrokerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => discountCodeService.AddAsync(dto));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidDiscountDateException>();
    }
    
    [Fact]
    public async Task AddAsync_GivenExistingDiscountCode_ShouldThrowDiscountCodeAlreadyExistsException()
    {
        //Arrange
        var dto = new DiscountCodeDto
        {
            Id = Guid.NewGuid(),
            Code = "TEST33",
            Description = "test code",
            Percentage = 33,
            ProductIds = new List<Guid> {Guid.NewGuid()},
            ValidFrom = _clock.CurrentDate().AddHours(2), 
            ValidTo = _clock.CurrentDate().AddDays(2).AddHours(2)
        };

        var discountCodeRepositoryMock = new Mock<IDiscountCodeRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();

        productRepositoryMock
            .Setup(x => x.GetAsync(dto.ProductIds))
            .ReturnsAsync(new List<Product> {new Product {Id = dto.ProductIds.First()}});
        
        discountCodeRepositoryMock
            .Setup(x => x.ExistsAsync(dto.Code))
            .ReturnsAsync(true);
        
        var discountCodeService = new DiscountCodeService(discountCodeRepositoryMock.Object, productRepositoryMock.Object,
            _dateValidator, messageBrokerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => discountCodeService.AddAsync(dto));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<DiscountCodeAlreadyExistsException>();
    }

    [Fact]
    public async Task DeleteAsync_GivenExistingDiscountCode_ShouldDeleteDiscountSuccessfully()
    {
        //Arrange
        var id = Guid.NewGuid();

        var discountCodeRepositoryMock = new Mock<IDiscountCodeRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        discountCodeRepositoryMock
            .Setup(x => x.GetAsync(id))
            .ReturnsAsync(new DiscountCode());

        var discountCodeService = new DiscountCodeService(discountCodeRepositoryMock.Object, productRepositoryMock.Object,
            _dateValidator, messageBrokerMock.Object);

        //Act
        await discountCodeService.DeleteAsync(id);
        
        //Assert
        discountCodeRepositoryMock.Verify(x => x.GetAsync(id), Times.Once);
        discountCodeRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<DiscountCode>()), Times.Once);
        messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<DiscountCodeExpired>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_GivenNoExistingDiscountCode_ShouldThrowDiscountCodeNotFoundException()
    {
        //Arrange
        var id = Guid.NewGuid();

        var discountCodeRepositoryMock = new Mock<IDiscountCodeRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        discountCodeRepositoryMock
            .Setup(x => x.GetAsync(id))
            .ReturnsAsync((DiscountCode)null);

        var discountCodeService = new DiscountCodeService(discountCodeRepositoryMock.Object, productRepositoryMock.Object,
            _dateValidator, messageBrokerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => discountCodeService.DeleteAsync(id));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<DiscountCodeNotFoundException>();
    }

    private readonly IClock _clock;
    private readonly DiscountDateValidator _dateValidator;

    public DiscountCodeServiceTests()
    {
        _clock = new TestClock();
        _dateValidator = new DiscountDateValidator(_clock);
    }
}