using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class ListExpensesValidator : AbstractValidator<UpdateExpensesRequest>
{
  public ListExpensesValidator()
  {
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
  }
}
