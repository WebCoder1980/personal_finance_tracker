using Microsoft.AspNetCore.Mvc;

namespace Transactions.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    [HttpGet(Name = "Get")]
    public string Get()
    {
        return "Ok";
    }
}
