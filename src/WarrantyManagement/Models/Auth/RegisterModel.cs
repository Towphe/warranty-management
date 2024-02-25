namespace WarrantyManagement.Models.Auth;

public class RegisterModel{
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string Username {get; set;}
    public required string Email {get; set;}
    public string? Password {get; set;} = null;
}