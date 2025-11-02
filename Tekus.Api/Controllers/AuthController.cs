using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Tekus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config) => _config = config;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        // Very simple: default user (for the test)
        var username = _config["Auth:DefaultUser:Username"] ?? "tekus.admin";
        var password = _config["Auth:DefaultUser:Password"] ?? "Tekus@123";

        if (dto.Username != username || dto.Password != password) return Unauthorized();

        var jwtKey = _config["Jwt:Key"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, dto.Username) }),
            Expires = DateTime.UtcNow.AddHours(int.Parse(_config["Jwt:ExpiryHours"] ?? "8")),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = creds
        });

        return Ok(new { token = tokenHandler.WriteToken(token) });
    }
}

public class LoginDto { public string Username { get; set; } = ""; public string Password { get; set; } = ""; }