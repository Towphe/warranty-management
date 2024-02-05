using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Models.Auth;
using WarrantyManagement.Models;
using WarrantyManagement.Services.Auth;
using System.Net;

namespace WarrantyManagement.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase{
    private readonly IAuthHandler _authHandler;
    
    public AuthController(IAuthHandler authHdlr){
        _authHandler = authHdlr;
    }

    [HttpPost("login")]
    public async Task<APIResponse> Login([FromBody] LoginModel login){
        var token = await _authHandler.Login(login);
        if (!token.Item1){
            // error
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new APIResponse(){
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Incorrect email or password."
            };
        }
        Response.StatusCode = (int)HttpStatusCode.Accepted;
        return new APIResponse(){
            StatusCode = HttpStatusCode.Accepted,
            Message = "Successfully logged in.",
            Data = new {
                Token = token.Item2
            }
        };    
    }
    [HttpPost("register")]
    public async Task<APIResponse> Register([FromBody] RegisterModel register){
        var res = await _authHandler.Register(register, 2);

        if (!res.Item1){
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new APIResponse(){
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Invalid registration."
            };
        }

        return new APIResponse(){
            StatusCode = HttpStatusCode.Accepted,
            Message = "Success",
            Data = res
        };
    }

    [Authorize(Roles = UserRoles.User)]
    [HttpGet("test")]
    public string TestEndpoint(){
        return "Authenticated as user.";
    }
}