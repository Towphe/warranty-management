using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WarrantyManagement.Models.Auth;
using WarrantyManagement.Models.Repo;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace WarrantyManagement.Services.Auth;

public class AuthHandler : IAuthHandler{
    private readonly IConfiguration _configuration;
    private readonly warrantyContext _dbContext;
    public AuthHandler(IConfiguration config, warrantyContext dbCtx){
        _configuration = config;
        _dbContext = dbCtx;
    }
    public string GenerateToken(TokenGenerationModel tokenModel){
        // get values from env
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = System.Text.Encoding.ASCII.GetBytes
        (_configuration["Jwt:Key"] ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // create oaykiad
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, tokenModel.Name),
                new Claim(JwtRegisteredClaimNames.Email, tokenModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, tokenModel.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };

        // generate token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        //var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);

        // return token
        return stringToken;
    }

    public async Task<(bool, string)> Login(LoginModel login){
        TokenGenerationModel? tokenGenerationModel = await (from u in _dbContext.Admins
                                                            select new TokenGenerationModel(){
                                                                Name = u.FirstName + " " + u.LastName,
                                                                Email = u.Email ?? "",
                                                                PasswordHash = u.Password,
                                                                Role = "Admin"
                                                            }).FirstOrDefaultAsync(ur => ur.Email == login.Email);
        if (tokenGenerationModel == null){
            // Return NonexistentUser error
            return (false, "NonexistentUser");
        }
        if (!BCrypt.Net.BCrypt.Verify(login.Password, tokenGenerationModel.PasswordHash))
        {
            // Return password mismatch error
            return (false, "PasswordMismatch");
        }

        // generate token and return it
        string token = GenerateToken(tokenGenerationModel);

        return (true, token);
    }

    public async Task<(bool, string)> Register(RegisterModel register, int role){
        var admin = await _dbContext.Admins.FirstOrDefaultAsync(u => u.Email == register.Email);

        if (admin != null){
            return (false, "EmailAlreadyExists");
        }

        admin = new Admin(){
            FirstName = register.FirstName,
            LastName = register.LastName,
            Username = register.Username,
            Email = register.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
        };

        await _dbContext.Admins.AddAsync(admin);

        await _dbContext.SaveChangesAsync();

        return (true, "LoginSuccess");
    }
}