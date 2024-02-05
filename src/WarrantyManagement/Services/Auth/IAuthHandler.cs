using WarrantyManagement.Models.Auth;

namespace WarrantyManagement.Services.Auth;

public interface IAuthHandler{
    public string GenerateToken(TokenGenerationModel tokenModel);
    public Task<(bool, string)> Login(LoginModel login);
    public Task<(bool, string)> Register(RegisterModel register, int role);
}