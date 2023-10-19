using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesResponse
{
  public ExpensesRecord _expenseRecord;

  public UpdateExpensesResponse(ExpensesRecord expenseRecord)
  {
    _expenseRecord = expenseRecord;
  }
}
