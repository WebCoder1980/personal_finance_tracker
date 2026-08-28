using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Transactions.Application.Categories.Handlers;
using PersonalFinanceTracker.Transactions.Application.Categories.Ports.In;
using PersonalFinanceTracker.Transactions.Infrastructure.Dtos;
using PersonalFinanceTracker.Transactions.Infrastructure.Util;

namespace PersonalFinanceTracker.Transactions.Infrastructure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private readonly ICategoryGetHandler _categoryGetHandler;
        private readonly ICategoryCreateHandler _categoryCreateHandler;
        private readonly ICategoryUpdateHandler _categoryUpdateHandler;
        private readonly ICategoryDeleteHandler _categoryDeleteHandler;

        public CategoryController(ICurrentUser currentUser, ICategoryGetHandler categoryGetHandler, ICategoryCreateHandler categoryCreateHandler, ICategoryUpdateHandler categoryUpdateHandler, ICategoryDeleteHandler categoryDeleteHandler)
        {
            _currentUser = currentUser;
            _categoryGetHandler = categoryGetHandler;
            _categoryCreateHandler = categoryCreateHandler;
            _categoryUpdateHandler = categoryUpdateHandler;
            _categoryDeleteHandler = categoryDeleteHandler;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoryGetResult>>> Get(CancellationToken token)
        {
            CategoryGetCommand command = new(_currentUser.Id);

            return Ok(await _categoryGetHandler.ExecuteAsync(command, token));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CategoryCreateResult>> Create(CategoryCreateRequest request, CancellationToken token)
        {
            CategoryCreateCommand command = new(_currentUser.Id, request.Name, request.Type, request.MonthlyAmount);

            return Ok(await _categoryCreateHandler.ExecuteAsync(command, token));
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult<CategoryUpdateResult>> UpdateById(Guid id, [FromBody] CategoryUpdateRequest request, CancellationToken token)
        {
            CategoryUpdateCommand command = new(id, _currentUser.Id, request.Name, request.MonthlyAmount);

            return Ok(await _categoryUpdateHandler.ExecuteAsync(command, token));
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteById(Guid id, CancellationToken token)
        {
            CategoryDeleteCommand command = new(id, _currentUser.Id);

            await _categoryDeleteHandler.ExecuteAsync(command, token);

            return Ok();
        }
    }
}
