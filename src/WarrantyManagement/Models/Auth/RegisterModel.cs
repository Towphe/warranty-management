namespace WarrantyManagement.Models.Auth;

public class RegisterModel{
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string? Email {get; set;} = null;
    public required string? Mobile {get; set;} = null;
    public string? Password {get; set;} = null;
    public int RoleId {get; set;} = 1;
    public required string Region {get; set;}
    public required string City {get; set;}
    public required string Brgy {get; set;}
    public required string Street {get; set;}
    public required int ZipCode {get; set;}
}