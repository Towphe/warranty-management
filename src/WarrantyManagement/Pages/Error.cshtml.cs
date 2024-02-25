using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WarrantyManagement.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public int Code {get; set;}
    public string Title {get; set;}
    public void OnGet([FromRoute] int code)
    {
        Code = code;
        switch (code){
            case 401:
                Title = "Unauthorized";
                break;
            default:
                Title = "Error";
                break;
        }
    }
}

