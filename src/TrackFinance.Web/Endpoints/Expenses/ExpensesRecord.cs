using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Expenses;

public record ExpensesRecord(
  int incomeId,
  string description,
  decimal amount,
  TransactionDescriptionType transactionDescriptionType,
  DateTime expenseDate,
  int userId,
  TransactionType transactionType
  );
