using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate.Specifications;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class GetById : EndpointBaseAsync
    .WithRequest<GetByIdExpensesRequest>
    .WithActionResult<GetByIdExpensesResponse>
{
  private readonly IRepository<Transaction> _repository;

  public GetById(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(GetByIdExpensesRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a single Expense",
      Description = "Gets a single Expense by Id",
      OperationId = "Expenses.GetById",
      Tags = new[] { "ExpensesEndpoints" })
  ]
  public override async Task<ActionResult<GetByIdExpensesResponse>> HandleAsync([FromRoute] GetByIdExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var transaction = new TransactionById(request.ExpenseId, TransactionType.Expense);
    var entity = await _repository.GetBySpecAsync(transaction, cancellationToken);
    if (entity == null) return NotFound();

    var response = new GetByIdExpensesResponse
    (
        description: entity.Description,
        amount: entity.Amount,
        transactionDescriptionType: entity.TransactionDescriptionType,
        expenseDate: entity.ExpenseDate
    );
    return Ok(response);
  }
}
