using ECommerce.Modules.Returns.Domain.Entities;
using ECommerce.Shared.Abstractions.Commands;

namespace ECommerce.Modules.Returns.Application.Commands;

public record ReturnProduct(Guid UserId, Guid OrderId, string Sku, ReturnType Type) : ICommand;