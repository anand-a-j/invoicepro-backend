using InvoicePro.Application.DTOs.Auth;
using InvoicePro.Application.Exceptions;
using InvoicePro.Application.interfaces;
using InvoicePro.Application.Interfaces.Identity;
using InvoicePro.Domain.Entities;
using InvoicePro.Infrastructure.Data.Repositories;


namespace InvoicePro.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwt
    )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterUserRequestDto req)
    {
        var existingUser = await _userRepository.GetByEmailAsync(req.Email);

        if (existingUser != null)
            throw new AppException(
            "Email already registered",
            System.Net.HttpStatusCode.BadRequest
            );

        var user = new User(
        req.Email,
        _passwordHasher.Hash(req.Password),
        req.FullName
        );

        await _userRepository.AddAsync(user);

        var userResponseDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
        };

        var authResponseDto = new AuthResponseDto
        {
            Token = _jwt.GenerateToken(user),
            User = userResponseDto
        };

        return authResponseDto;
    }
}