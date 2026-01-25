using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvoicePro.Application.Exceptions;
using InvoicePro.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator
{
    private readonly IConfiguration _config;

    public JwtTokenGenerator(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
              new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
              new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim("role", user.Role.ToString()),
          };

        var keyString = _config["Jwt:key"];

        if (string.IsNullOrEmpty(keyString))
            throw new AppException("Jwt key not configured");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}