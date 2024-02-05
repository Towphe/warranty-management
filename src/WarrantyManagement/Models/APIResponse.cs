using System.Net;

namespace WarrantyManagement.Models;

public class APIResponse{
    public required HttpStatusCode StatusCode {get; set;}
    public required string Message {get; set;}
    public dynamic? Data {get; set;} = null;
}