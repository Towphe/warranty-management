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
    private readonly WarrantydbContext _dbContext;
    public AuthHandler(IConfiguration config, WarrantydbContext dbCtx){
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
        TokenGenerationModel? tokenGenerationModel = await (from u in _dbContext.Users
                                                            join r in _dbContext.Roles on u.RoleId equals r.Id
                                                            select new TokenGenerationModel(){
                                                                Name = u.FirstName + " " + u.LastName,
                                                                Email = u.Email ?? "",
                                                                PasswordHash = u.Password,
                                                                Role = r.Name
                                                            }).FirstOrDefaultAsync(ur => ur.Email == login.Email);
        if (tokenGenerationModel == null){
            // Return NonexistentUser error
            Console.WriteLine("Here 1");
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
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == register.Email);

        if (user != null){
            return (false, "EmailAlreadyExists");
        }

        user = new User(){
            FirstName = register.FirstName,
            LastName = register.LastName,
            Email = register.Email,
            Mobile = register.Mobile,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
            RoleId = role
        };

        await _dbContext.Users.AddAsync(user);
        
        await _dbContext.SaveChangesAsync();

        user = await _dbContext.Users.FirstAsync(u => u.Email == register.Email);

        await _dbContext.Addresses.AddAsync(new Address(){
            UserId = user.Id,
            Region = register.Region,
            City = register.City,
            Brgy = register.Brgy,
            Street = register.Street,
            Zipcode = register.ZipCode
        });

        await _dbContext.SaveChangesAsync();

        return (true, "LoginSuccess");
    }
}