﻿
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using Xunit;

namespace TrackFinance.IntegrationTests.Data;
public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {

    var repository = GetRepository();
    var transaction = new Transaction("TestTransaction", 2000, TransactionDescriptionType.Education, DateTime.Now, 1, TransactionType.Expense);

    await repository.AddAsync(transaction);

    var newTransaction = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal("TestTransaction", newTransaction?.Description);
    Assert.True(newTransaction?.Id > 0);
  }
}
