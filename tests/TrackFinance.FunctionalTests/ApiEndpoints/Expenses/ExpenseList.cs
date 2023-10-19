using Newtonsoft.Json;
using TrackFinance.Web;
using TrackFinance.Web.Endpoints.Expense;
using TrackFinance.Web.Endpoints.Expenses;
using Xunit;

namespace TrackFinance.FunctionalTests.ApiEndpoints.Expenses;
public class ExpenseList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ExpenseList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnExpensesListValuesSuccess()
  {
    var response = await _client.GetStringAsync(ListExpensesRequest.BuildRoute(1));
    var result = JsonConvert.DeserializeObject<ListExpensesResponse>(response);
    Assert.True(result!.Expenses.Count > 0);
  }

  [Fact]
  public async Task ReturnExpensesListEmptySuccess()
  {
    var response = await _client.GetStringAsync(ListExpensesRequest.BuildRoute(100));
    var result = JsonConvert.DeserializeObject<ListExpensesResponse>(response);
    Assert.True(result!.Expenses.Count == 0);
  }

  [Fact]
  public async Task SearchExpenseValueSuccess()
  {
    var response = await _client.GetStringAsync(ListExpensesRequest.BuildRoute(1));
    Assert.Contains("computadoras", response);
  }
}
