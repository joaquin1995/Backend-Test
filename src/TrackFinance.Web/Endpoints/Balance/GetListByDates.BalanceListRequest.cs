using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByDatesRequest
{
  public const string Route = "/Balance/{InitDate}/{EndDate}/{UserId}/{TransactionType}";
  public DateTime InitDate { get; set; }
  public DateTime EndDate { get; set; }
  public int UserId { get; set; }
  public TransactionType TransactionType { get; set; }
}
