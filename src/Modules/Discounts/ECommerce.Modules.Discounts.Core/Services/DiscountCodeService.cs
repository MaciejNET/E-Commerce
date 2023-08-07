using ECommerce.Modules.Discounts.Core.DTO;
using ECommerce.Modules.Discounts.Core.Entities;
using ECommerce.Modules.Discounts.Core.Events;
using ECommerce.Modules.Discounts.Core.Exceptions;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Modules.Discounts.Core.Validators;
using ECommerce.Shared.Abstractions.Messaging;

namespace ECommerce.Modules.Discounts.Core.Services;

internal class DiscountCodeService : IDiscountCodeService
{
    private readonly IDiscountCodeRepository _discountCodeRepository;
    private readonly IProductRepository _productRepository;
    private readonly DiscountDateValidator _dateValidator;
    private readonly IMessageBroker _messageBroker;

    public DiscountCodeService(IDiscountCodeRepository discountCodeRepository, IProductRepository productRepository, DiscountDateValidator dateValidator, IMessageBroker messageBroker)
    {
        _discountCodeRepository = discountCodeRepository;
        _productRepository = productRepository;
        _dateValidator = dateValidator;
        _messageBroker = messageBroker;
    }

    public async Task AddAsync(DiscountCodeDto dto)
    {
        List<Product> products = new();
        if (dto.ProductIds.Any())
        {
            products = (await _productRepository.GetAsync(dto.ProductIds)).ToList();

            if (products.Count != dto.ProductIds.Count)
            {
                throw new ProductsNotFoundException();
            }
        }

        var isDateValid = _dateValidator.Validate(dto.ValidFrom, dto.ValidTo);

        if (!isDateValid)
        {
            throw new InvalidDiscountDateException();
        }

        var exists = await _discountCodeRepository.ExistsAsync(dto.Code);

        if (exists)
        {
            throw new DiscountCodeAlreadyExistsException(dto.Code);
        }

        dto.Id = Guid.NewGuid();
        var discountCode = new DiscountCode
        {
            Id = dto.Id,
            Code = dto.Code,
            Description = dto.Description,
            Percentage = dto.Percentage,
            Products = products.Count == 0 ? null : products,
            ValidFrom = dto.ValidFrom,
            ValidTo = dto.ValidTo
        };
        await _discountCodeRepository.AddAsync(discountCode);
    }

    public async Task DeleteAsync(Guid id)
    {
        var discountCode = await _discountCodeRepository.GetAsync(id);

        if (discountCode is null)
        {
            throw new DiscountCodeNotFoundException(id);
        }

        await _discountCodeRepository.DeleteAsync(discountCode);
        await _messageBroker.PublishAsync(new DiscountCodeExpired(id));
    }
}