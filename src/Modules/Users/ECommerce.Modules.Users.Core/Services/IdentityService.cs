using ECommerce.Modules.Users.Core.DTO;
using ECommerce.Modules.Users.Core.Entities;
using ECommerce.Modules.Users.Core.Events;
using ECommerce.Modules.Users.Core.Exceptions;
using ECommerce.Modules.Users.Core.Repositories;
using ECommerce.Modules.Users.Core.Validators;
using ECommerce.Shared.Abstractions.Auth;
using ECommerce.Shared.Abstractions.Messaging;
using ECommerce.Shared.Abstractions.Time;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Modules.Users.Core.Services;

internal class IdentityService : IIdentityService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IAuthManager _authManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly PasswordValidator _passwordValidator;

    public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthManager authManager, IClock clock, IMessageBroker messageBroker, PasswordValidator passwordValidator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _authManager = authManager;
        _clock = clock;
        _messageBroker = messageBroker;
        _passwordValidator = passwordValidator;
    }

    public async Task<AccountDto> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);

        return user is null
            ? null
            : new AccountDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Claims = user.Claims,
                PreferedCurrency = user.PreferredCurrency,
                CreatedAt = user.CreatedAt
            };
    }

    public async Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        var user = await _userRepository.GetAsync(dto.Email.ToLowerInvariant());
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (_passwordHasher.VerifyHashedPassword(default, user.Password, dto.Password) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        if (!user.IsActive)
        {
            throw new UserNotActiveException(user.Id);
        }

        var jwt = _authManager.CreateToken(user.Id.ToString(), user.Role, claims: user.Claims);
        jwt.Email = user.Email;
        await _messageBroker.PublishAsync(new SignedIn(user.Id));

        return jwt;
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        dto.Id = Guid.NewGuid();
        var email = dto.Email.ToLowerInvariant();
        var user = await _userRepository.GetAsync(email);
        if (user is not null)
        {
            throw new EmailInUseException();
        }
        
        var isPasswordValid = _passwordValidator.Validate(dto.Password);
        if (!isPasswordValid)
        {
            throw new InvalidPasswordException();
        }
        
        var password = _passwordHasher.HashPassword(default, dto.Password);
        user = new User
        {
            Id = dto.Id,
            FullName = dto.FullName,
            Email = email,
            Password = password,
            Role = dto.Role?.ToLowerInvariant() ?? "user",
            CreatedAt = _clock.CurrentDate(),
            PreferredCurrency = dto.PreferredCurrency,
            IsActive = true,
            Claims = dto.Claims ?? new Dictionary<string, IEnumerable<string>>()
        };
        await _userRepository.AddAsync(user);
        await _messageBroker.PublishAsync(new SignedUp(user.Id, user.Email, user.PreferredCurrency));
    }
}