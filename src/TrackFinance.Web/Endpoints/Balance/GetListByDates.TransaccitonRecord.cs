using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public record TransactionRecordDates(DateTime Date, DayOfWeek? DayOfWeek, int Day, decimal TotalAmount, 
  TransactionDescriptionType TransactionDescriptionType, int Week, int Year, int Month);
