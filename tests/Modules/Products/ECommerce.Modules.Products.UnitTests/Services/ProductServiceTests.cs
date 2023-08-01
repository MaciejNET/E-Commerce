using ECommerce.Modules.Products.Core.DTO;
using ECommerce.Modules.Products.Core.Entities;
using ECommerce.Modules.Products.Core.Events;
using ECommerce.Modules.Products.Core.Exceptions;
using ECommerce.Modules.Products.Core.Repositories;
using ECommerce.Modules.Products.Core.Services;
using ECommerce.Shared.Abstractions.Messaging;
using FluentAssertions;
using Moq;

namespace ECommerce.Modules.Products.UnitTests.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task AddAsync_GivenProductDoesNotExists_ShouldAddProductSuccessfully()
    {
        //Arrange
        var productDetailsDto = new ProductDetailsDto
        {
            Category = "test",
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 24.54M,
            ImageUrl = "test/test",
            StockQuantity = 123
        };
        
        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.ExistsAsync(productDetailsDto.Sku))
            .ReturnsAsync(false);

        categoryRepositoryMock
            .Setup(x => x.GetAsync(productDetailsDto.Category))
            .ReturnsAsync(new Category {Id = Guid.NewGuid(), Name = productDetailsDto.Category});

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        await productService.AddAsync(productDetailsDto);

        //Assert
        productRepositoryMock.Verify(x => x.ExistsAsync(productDetailsDto.Sku), Times.Once);
        categoryRepositoryMock.Verify(x => x.GetAsync(productDetailsDto.Category), Times.Once);
        productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Product>()));
        messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<ProductCreated>()), Times.Once);
    }
    
    [Fact]
    public async Task AddAsync_GivenExistingProduct_ShouldThrowProductAlreadyExistsException()
    {
        //Arrange
        var productDetailsDto = new ProductDetailsDto
        {
            Category = "test",
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 24.54M,
            ImageUrl = "test/test",
            StockQuantity = 123
        };
        
        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.ExistsAsync(productDetailsDto.Sku))
            .ReturnsAsync(true);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        var exceptions = await Record.ExceptionAsync(() => productService.AddAsync(productDetailsDto));

        //Assert
        exceptions.Should().NotBeNull();
        exceptions.Should().BeOfType<ProductAlreadyExistsException>();
    }
    
    [Fact]
    public async Task AddAsync_GivenNoExistingCategory_ShouldThrowCategoryNotFoundException()
    {
        //Arrange
        var productDetailsDto = new ProductDetailsDto
        {
            Category = "test",
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 24.54M,
            ImageUrl = "test/test",
            StockQuantity = 123
        };
        
        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.ExistsAsync(productDetailsDto.Sku))
            .ReturnsAsync(false);
        
        categoryRepositoryMock
            .Setup(x => x.GetAsync(productDetailsDto.Category))
            .ReturnsAsync((Category)null);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        var exceptions = await Record.ExceptionAsync(() => productService.AddAsync(productDetailsDto));

        //Assert
        exceptions.Should().NotBeNull();
        exceptions.Should().BeOfType<CategoryNotFoundException>();
    }

    [Fact]
    public async Task UpdateAsync_GivenValidProduct_ShouldUpdateProductSuccessfully()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var existingCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "test"
        };
        
        var existingProduct = new Product
        {
            Id = productId,
            CategoryId = existingCategory.Id,
            Category = existingCategory,
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 36.33M,
            StockQuantity = 111,
            ImageUrl = "test/test"
        };
        
        var productDetailsDto = new ProductDetailsDto
        {
            Id = productId,
            Category = "test",
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 24.54M,
            ImageUrl = "test/test",
            StockQuantity = 123
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.GetAsync(productId))
            .ReturnsAsync(existingProduct);

        categoryRepositoryMock
            .Setup(x => x.GetAsync(productDetailsDto.Category))
            .ReturnsAsync(existingCategory);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        await productService.UpdateAsync(productDetailsDto);

        //Assert
        productRepositoryMock.Verify(x => x.GetAsync(productDetailsDto.Id), Times.Once);
        categoryRepositoryMock.Verify(x => x.GetAsync(productDetailsDto.Category), Times.Once);
        productRepositoryMock.Verify(x => x.UpdateAsync(existingProduct));
        messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<ProductUpdated>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateAsync_GivenNoExistingProduct_ShouldThrowProductNotFoundException()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var productDetailsDto = new ProductDetailsDto
        {
            Id = productId,
            Category = "test",
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 24.54M,
            ImageUrl = "test/test",
            StockQuantity = 123
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.GetAsync(productId))
            .ReturnsAsync((Product)null);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => productService.UpdateAsync(productDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ProductNotFoundException>();
    }
    
    [Fact]
    public async Task UpdateAsync_GivenNoExistingCategory_ShouldThrowCategoryNotFoundException()
    {
        //Arrange
        var productId = Guid.NewGuid();
        
        var existingProduct = new Product
        {
            Id = productId,
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 36.33M,
            StockQuantity = 111,
            ImageUrl = "test/test"
        };
        
        var productDetailsDto = new ProductDetailsDto
        {
            Id = productId,
            Category = "test",
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 24.54M,
            ImageUrl = "test/test",
            StockQuantity = 123
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.GetAsync(productId))
            .ReturnsAsync(existingProduct);

        categoryRepositoryMock
            .Setup(x => x.GetAsync(productDetailsDto.Category))
            .ReturnsAsync((Category) null);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => productService.UpdateAsync(productDetailsDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<CategoryNotFoundException>();
    }
    
    [Fact]
    public async Task DeleteAsync_GivenExistingProduct_ShouldDeleteProductSuccessfully()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product
        {
            Id = productId,
            Name = "test",
            Manufacturer = "test",
            Description = "test",
            Sku = "test",
            Price = 36.33M,
            StockQuantity = 111,
            ImageUrl = "test/test"
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.GetAsync(productId))
            .ReturnsAsync(existingProduct);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        await productService.DeleteAsync(productId);

        //Assert
        productRepositoryMock.Verify(x => x.GetAsync(productId), Times.Once);
        productRepositoryMock.Verify(x => x.UpdateAsync(existingProduct));
        messageBrokerMock.Verify(x => x.PublishAsync(It.IsAny<ProductDeleted>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_GivenNoExistingProduct_ShouldThrowProductNotFoundException()
    {
        //Arrange
        var productId = Guid.NewGuid();

        var productRepositoryMock = new Mock<IProductRepository>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        var messageBrokerMock = new Mock<IMessageBroker>();
        
        productRepositoryMock
            .Setup(x => x.GetAsync(productId))
            .ReturnsAsync((Product)null);

        var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, messageBrokerMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => productService.DeleteAsync(productId));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ProductNotFoundException>();
    }
}