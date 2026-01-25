using InvoicePro.Application.DTOs.Auth;

namespace InvoicePro.Application.interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterUserRequestDto req);
}