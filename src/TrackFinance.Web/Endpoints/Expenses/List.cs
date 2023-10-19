using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class List : EndpointBaseAsync
    .WithRequest<ListExpensesRequest>
    .WithActionResult<ListExpensesResponse>
{
  private readonly IReadRepository<Transaction> _repository;

  public List(IReadRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(ListExpensesRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a list of all Expenses",
      Description = "Gets a list of all Expenses",
      OperationId = "Expenses.List",
      Tags = new[] { "ExpensesEndpoints" })
  ]
  public async override Task<ActionResult<ListExpensesResponse>> HandleAsync([FromRoute] ListExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var response = new ListExpensesResponse();
    response.Expenses = (await _repository.ListAsync(cancellationToken))
        .Where(expense => expense.UserId == request.UserId && expense.TransactionType == TransactionType.Expense)
        .Select(expense => new ExpensesRecord(incomeId: expense.Id,
                                           description: expense.Description,
                                           amount: expense.Amount,
                                           transactionDescriptionType: expense.TransactionDescriptionType,
                                           expenseDate: expense.ExpenseDate,
                                           userId: expense.UserId,
                                           transactionType: expense.TransactionType))
        .ToList();

    return Ok(response);
  }
}
