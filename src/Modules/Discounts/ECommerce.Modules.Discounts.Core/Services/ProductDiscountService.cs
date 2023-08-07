using ECommerce.Modules.Discounts.Core.DTO;
using ECommerce.Modules.Discounts.Core.Entities;
using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Exceptions;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Modules.Discounts.Core.Validators;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Modules.Discounts.Core.Services;

internal class ProductDiscountService : IProductDiscountService
{
    private readonly IProductDiscountRepository _productDiscountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly DiscountDateValidator _dateValidator;

    public ProductDiscountService(IProductDiscountRepository productDiscountRepository, IProductRepository productRepository, IMessageBroker messageBroker, DiscountDateValidator dateValidator)
    {
        _productDiscountRepository = productDiscountRepository;
        _productRepository = productRepository;
        _messageBroker = messageBroker;
        _dateValidator = dateValidator;
    }

    public async Task AddAsync(ProductDiscountDto dto)
    {
        var product = await _productRepository.GetAsync(dto.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException(dto.ProductId);
        }

        var isDateValid = _dateValidator.Validate(dto.ValidFrom, dto.ValidTo);

        if (!isDateValid)
        {
            throw new InvalidDiscountDateException();
        }

        var canAddDiscountForProduct = await _productDiscountRepository.CanAddDiscountForProductAsync(dto.ProductId, dto.ValidFrom, dto.ValidTo);

        if (!canAddDiscountForProduct)
        {
            throw new ProductAlreadyHasDiscountException(dto.ProductId);
        }

        dto.Id = Guid.NewGuid();
        var productDiscount = new ProductDiscount
        {
            Id = dto.Id,
            NewPrice = dto.NewPrice,
            ProductId = product.Id,
            Product = product,
            ValidFrom = dto.ValidFrom,
            ValidTo = dto.ValidTo
        };
        await _productDiscountRepository.AddAsync(productDiscount);
        await _messageBroker.PublishAsync(new ProductDiscountAdded(productDiscount.ProductId, productDiscount.NewPrice));
    }

    public async Task DeleteAsync(Guid id)
    {
        var productDiscount = await _productDiscountRepository.GetAsync(id);

        if (productDiscount is null)
        {
            throw new ProductDiscountNotFoundException(id);
        }

        await _productDiscountRepository.DeleteAsync(productDiscount);
        await _messageBroker.PublishAsync(new ProductDiscountExpired(productDiscount.ProductId));
    }
}