using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fundo.Application.DTOs;
using Fundo.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fundo.API.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IOptions<JwtSettings> jwtOptions) : ControllerBase
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    [HttpPost("token")]
    [AllowAnonymous]
    public IActionResult Token([FromBody] ClientAuthRequest request)
    {
        // Mock check - você pode validar via banco depois
        if (request.ClientId != "fundo-app" || request.ClientSecret != "dev-secret-123")
            return Unauthorized("Invalid client credentials");

        var claims = new[]
        {
            new Claim("client_id", request.ClientId)
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: creds
        );

        return Ok(new
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(token),
            expires_in = _jwtSettings.ExpirationMinutes * 60
        });
    }
}