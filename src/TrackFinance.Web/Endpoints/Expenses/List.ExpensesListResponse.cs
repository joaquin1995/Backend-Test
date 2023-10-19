using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class ListExpensesResponse
{
  public List<ExpensesRecord> Expenses { get; set; } = new();

}
