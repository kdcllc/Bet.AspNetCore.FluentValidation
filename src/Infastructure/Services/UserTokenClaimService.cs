using Infastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infastructure.Services;

public class UserTokenClaimService
{
    private readonly TodoDbContext _context;
    private readonly IConfiguration _configuration;

    public UserTokenClaimService(
        TodoDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string?> GetTokenAsync(string userName, string password)
    {
        if (!string.IsNullOrEmpty(userName) &&
            !string.IsNullOrEmpty(password))
        {
            var loggedInUser = await _context.Users
                                             .FirstOrDefaultAsync(user => user.Username == userName
                                                                    && user.Password == password);

            if (loggedInUser == null)
            {
                return null;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loggedInUser.Username),
                new Claim("name", loggedInUser.Username),
                new Claim(JwtRegisteredClaimNames.Email, loggedInUser.Email)
            };

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        return null;
    }
}
