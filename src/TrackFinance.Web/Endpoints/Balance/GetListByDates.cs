using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByDates : EndpointBaseAsync
    .WithRequest<GetListByDatesRequest>
    .WithActionResult<GetListByDatesResponse>
{

  private readonly ITransactionFinanceService _transactionService;

  public GetListByDates(ITransactionFinanceService transactionService)
  {
    _transactionService = transactionService;
  }

  [HttpGet(GetListByDatesRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
    Summary = "group",
    Description = "group",
    OperationId = "BalanceDates.List",
    Tags = new[] { "BalanceEndpoints" })
]

  public override async Task<ActionResult<GetListByDatesResponse>> HandleAsync([FromRoute] GetListByDatesRequest request, CancellationToken cancellationToken = default)
  {
    var transactions = await _transactionService.GetTransactionItemsByDatesAsync(request.UserId, request.TransactionType, request.InitDate, request.EndDate, cancellationToken);

    if (transactions.Status != ResultStatus.Ok) return BadRequest(transactions.ValidationErrors);

    return Ok(new GetListByDatesResponse
    {
      ExpensesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Expense),
      IncomesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Income)
    });
  }

  private static List<TransactionRecordDates>? GetTransactionsRecords(List<TransactionDataDto> transactions, TransactionType transactionType)
  {
    return new List<TransactionRecordDates>(transactions.Where(x => x.TransactionType == transactionType)
                       .Select(item => new TransactionRecordDates(
                        item.Date,
                        item.DayOfWeek,
                        item.Day,
                        item.TotalAmount,
                        item.TransactionDescriptionType,
                        item.Week,
                        item.Year,
                        item.Month
                       )));
  }
}
