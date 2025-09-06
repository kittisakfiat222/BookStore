using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace BookStore.Services;
public class JwtService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public JwtService(IConfiguration cfg)
    {
        _key = cfg["Jwt:Key"] ?? throw new Exception("Missing Jwt:Key");
        _issuer = cfg["Jwt:Issuer"] ?? "bookstore";
        _audience = cfg["Jwt:Audience"] ?? "itbookshop_api";
    }

    public string GenerateToken(int userId, string username, string fullname, TimeSpan? lifetime = null)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim("username", username),
            new Claim("fullname", fullname)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.Add(lifetime ?? TimeSpan.FromHours(12));

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
