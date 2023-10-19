
using Ardalis.Specification;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Infrastructure.Data;
public class CachedRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{
  private readonly IMemoryCache _cache;
  private readonly ILogger<CachedRepository<T>> _logger;
  private readonly EfRepository<T> _sourceRepository;
  private MemoryCacheEntryOptions _cacheOptions;

  public CachedRepository(IMemoryCache cache,
      ILogger<CachedRepository<T>> logger,
      EfRepository<T> sourceRepository)
  {
    _cache = cache;
    _logger = logger;
    _sourceRepository = sourceRepository;

    _cacheOptions = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(relative: TimeSpan.FromMinutes(10));
  }

  public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return _sourceRepository.AnyAsync(specification, cancellationToken);
  }

  public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
  {
    return _sourceRepository.AnyAsync(cancellationToken);
  }

  public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return _sourceRepository.CountAsync(specification, cancellationToken);
  }

  public Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    return _sourceRepository.CountAsync(cancellationToken);
  }

  public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
  {
    return _sourceRepository.GetByIdAsync(id, cancellationToken);
  }

  public Task<T?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
  {
    if (specification.CacheEnabled)
    {
      var key = $"{specification.CacheKey}-GetBySpecAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
  }

  public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
  {
    var key = $"{nameof(T)}-ListAsync";
    return _cache.GetOrCreate(key, entry =>
    {
      entry.SetOptions(_cacheOptions);
      return _sourceRepository.ListAsync(cancellationToken);
    });
  }

  public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      var key = $"{specification.CacheKey}-ListAsync";
      //_logger.LogInformation("Checking cache for " + key);
      return _cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        //_logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.ListAsync(specification, cancellationToken);
  }

  public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
