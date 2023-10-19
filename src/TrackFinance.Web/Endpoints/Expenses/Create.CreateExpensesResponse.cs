namespace TrackFinance.Web.Endpoints.Expenses;

public class CreateExpensesResponse
{
  public CreateExpensesResponse(int statusResult, int expensesId)
  {
    StatusResult = statusResult;
    ExpensesId = expensesId;
  }

  public int StatusResult { get; set; }
  public int ExpensesId { get; set; }
}
