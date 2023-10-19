using FluentValidation;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByDatesValidator : AbstractValidator<GetListByDatesRequest>
{
  public GetListByDatesValidator()
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
