namespace TrackFinance.Web.Endpoints.Expenses;

public class ListExpensesRequest
{
  public const string Route = "/Expenses/{UserId:int}/user";
  public static string BuildRoute(int userId) => Route.Replace("{UserId:int}", userId.ToString());
  public int UserId { get; set; }
}
