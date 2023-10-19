using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class ItemsByDatesSpec : Specification<Transaction>, ISingleResultSpecification
{
  public ItemsByDatesSpec(int userId, TransactionType transactionType, DateTime initDate, DateTime endDate)
  {
    Query.Where(h => h.ExpenseDate.Date >= initDate && h.ExpenseDate.Date <= endDate)
         .Where(h => (transactionType == TransactionType.All) || h.TransactionType == transactionType)
         .Where(h => h.UserId == userId)
         .OrderBy(g => g.ExpenseDate);
  }
}
