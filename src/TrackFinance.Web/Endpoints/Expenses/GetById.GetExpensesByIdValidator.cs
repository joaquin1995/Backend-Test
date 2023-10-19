using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class GetByIdExpensesValidator : AbstractValidator<GetByIdExpensesRequest>
{
  public GetByIdExpensesValidator()
  {
    RuleFor(expense => expense.ExpenseId).GreaterThan(0);
  }
}

