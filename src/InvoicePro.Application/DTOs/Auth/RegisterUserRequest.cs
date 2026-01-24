namespace InvoicePro.Application.DTOs.Auth;

public class RegisterUserRequestDto
{
    public string Email {get; set;} = null!;
    public string Password {get; set;} = null!;
    public string FullName {get;set;} = null!;
}