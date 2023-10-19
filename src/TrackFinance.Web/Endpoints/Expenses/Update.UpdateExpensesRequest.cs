using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesRequest
{
  public const string Route = "/Expenses";
  public int Id { get; set; }
  public string Description { get; set; } = string.Empty;
  public decimal Amount { get; set; }
  public TransactionDescriptionType ExpenseType { get; set; }
  public DateTime ExpenseDate { get; set; }
  public int UserId { get; set; }
}
