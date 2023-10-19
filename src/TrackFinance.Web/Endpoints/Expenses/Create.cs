using System.Net;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Create : EndpointBaseAsync
    .WithRequest<CreateExpensesRequest>
    .WithActionResult<CreateExpensesResponse>

{
  private readonly IRepository<Transaction> _repository;

  public Create(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPost("/Expenses")]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "Creates a new Expenses",
    Description = "Creates a new Expenses",
    OperationId = "Expenses.Create",
    Tags = new[] { "ExpensesEndpoints" })
  ]
  public async override Task<ActionResult<CreateExpensesResponse>> HandleAsync(CreateExpensesRequest request, CancellationToken cancellationToken = default)
  {
    var newExpense = new Transaction(request.Description,
                                    request.Amount,
                                    request.ExpenseType,
                                    request.ExpenseDate,
                                    request.UserId,
                                    TransactionType.Expense);
    var createdItem = await _repository.AddAsync(newExpense, cancellationToken);

    var response = new CreateExpensesResponse
    (
    statusResult: (int)HttpStatusCode.OK,
    expensesId: createdItem.Id
    );

    return Ok(response);
  }
}
