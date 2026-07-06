using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Domain.Dtos;
using PersonalFinanceTracker.Domain.Models;
using PersonalFinanceTracker.Transactions.Service;

namespace PersonalFinanceTracker.Transactions.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Category>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAsync(cancellationToken));
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Category>> Create(CategoryUpsertRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _service.CreateAsync(request, cancellationToken));
    }
    
    [HttpPatch("{id:long}")]
    [Authorize]
    public async Task<ActionResult<Category>> UpdateById(long id, [FromBody] JsonPatchDocument<CategoryUpsertRequest> patchDoc, CancellationToken cancellationToken)
    {
        return Ok(await _service.UpdateAsync(id, patchDoc, cancellationToken));
    }

    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<ActionResult> DeleteById(long id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return Ok();
    }

}
