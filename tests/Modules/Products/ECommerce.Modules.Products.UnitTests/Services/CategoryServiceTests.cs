using ECommerce.Modules.Products.Core.DTO;
using ECommerce.Modules.Products.Core.Entities;
using ECommerce.Modules.Products.Core.Exceptions;
using ECommerce.Modules.Products.Core.Repositories;
using ECommerce.Modules.Products.Core.Services;
using FluentAssertions;
using Moq;

namespace ECommerce.Modules.Products.UnitTests.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task AddAsync_GivenCategoryDoesNotExists_ShouldAddCategorySuccessfully()
    {
        //Arrange
        var categoryDto = new CategoryDto
        {
            Id = Guid.NewGuid(),
            Name = "test"
        };

        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        categoryRepositoryMock
            .Setup(x => x.ExistsByNameAsync(categoryDto.Name))
            .ReturnsAsync(false);

        var categoryService = new CategoryService(categoryRepositoryMock.Object);
        
        //Act
        await categoryService.AddAsync(categoryDto);

        //Assert
        categoryRepositoryMock.Verify(x => x.ExistsByNameAsync(categoryDto.Name), Times.Once);
        categoryRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_GivenExistingCategory_ShouldThrowCategoryAlreadyExistsException()
    {
        //Arrange
        var categoryDto = new CategoryDto
        {
            Id = Guid.NewGuid(),
            Name = "test"
        };

        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        categoryRepositoryMock
            .Setup(x => x.ExistsByNameAsync(categoryDto.Name))
            .ReturnsAsync(true);

        var categoryService = new CategoryService(categoryRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => categoryService.AddAsync(categoryDto));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<CategoryAlreadyExistsException>();
    }
}