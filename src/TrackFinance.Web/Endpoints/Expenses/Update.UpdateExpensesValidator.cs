using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesValidator : AbstractValidator<UpdateExpensesRequest>
{
  public UpdateExpensesValidator()
  {
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
  }
}
