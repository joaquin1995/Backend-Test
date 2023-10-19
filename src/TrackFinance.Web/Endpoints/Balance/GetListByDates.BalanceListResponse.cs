namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByDatesResponse
{
  public List<TransactionRecordDates>? ExpensesTransaction { get; set; }
  public List<TransactionRecordDates>? IncomesTransaction { get; set; }
}
