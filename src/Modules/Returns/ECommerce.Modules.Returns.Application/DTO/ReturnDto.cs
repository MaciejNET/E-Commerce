using ECommerce.Modules.Returns.Domain.Entities;

namespace ECommerce.Modules.Returns.Application.DTO;

public record ReturnDto(Guid Id, Guid UserId, Guid OrderId, string Sku, ReturnType Type, ReturnStatus Status);