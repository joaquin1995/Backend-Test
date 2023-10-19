using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateExpensesRequest>
    .WithActionResult<UpdateExpensesResponse>
{

  private readonly IRepository<Transaction> _repository;

  public Update(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateExpensesRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
     Summary = "Updates a Expenses",
     Description = "Updates a Expenses",
     OperationId = "Expenses.Update",
     Tags = new[] { "ExpensesEndpoints" })
  ]
  public async override Task<ActionResult<UpdateExpensesResponse>> HandleAsync(UpdateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var existingIncomes = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingIncomes == null)
    {
      return NotFound();
    }

    existingIncomes.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Expense);

    await _repository.UpdateAsync(existingIncomes, cancellationToken);

    var response = new UpdateExpensesResponse(
        expenseRecord: new ExpensesRecord(
          existingIncomes.Id,
          existingIncomes.Description,
          existingIncomes.Amount,
          existingIncomes.TransactionDescriptionType,
          existingIncomes.ExpenseDate,
          existingIncomes.UserId,
          existingIncomes.TransactionType));

    return Ok(response);
  }
}
