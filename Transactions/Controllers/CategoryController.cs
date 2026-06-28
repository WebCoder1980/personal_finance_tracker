using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transactions.Models;
using Transactions.Service;

namespace Transactions.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }


    [HttpGet(Name = "Get")]
    [Authorize]
    public async Task<ActionResult<IReadOnlyCollection<Category>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAsync(cancellationToken));
    }
}
