using System;
using System.Threading;
using System.Threading.Tasks;
using AniDroid.AniList.Interfaces;
using AniDroid.AniList.Models;
using OneOf;

namespace AniDroid.AniList.Utils
{
    // TODO: Use IAsyncEnumerator+IAsyncEnumnerable from C# 8.0 ASAP
    internal class PagedAsyncEnumerable<T> : IAsyncEnumerable<AniListObject.PagedData<T>>
    {
        private readonly Func<PagingInfo, CancellationToken, Task<OneOf<AniListObject.PagedData<T>, IAniListError>>> _getPage;
        private readonly Func<PagingInfo, AniListObject.PagedData<T>, bool> _nextPage;

        public int PageSize { get; }

        public PagedAsyncEnumerable(int pageSize,
            Func<PagingInfo, CancellationToken, Task<OneOf<AniListObject.PagedData<T>, IAniListError>>> getPage,
            Func<PagingInfo, AniListObject.PagedData<T>, bool> nextPage)
        {
            if (pageSize <= 0) throw new ArgumentException($"Value cannot be less than or equal to zero (0)", nameof(pageSize));
            PageSize = pageSize;
            _getPage = getPage ?? throw new ArgumentNullException(nameof(getPage));
            _nextPage = nextPage ?? throw new ArgumentNullException(nameof(nextPage));
        }

        public IAsyncEnumerator<AniListObject.PagedData<T>> GetEnumerator()
            => new Enumerator(this);

        public class Enumerator : IAsyncEnumerator<AniListObject.PagedData<T>>
        {
            private readonly PagedAsyncEnumerable<T> _source;
            private readonly PagingInfo _info;

            public AniListObject.PagedData<T> Current { get; private set; }

            public Enumerator(PagedAsyncEnumerable<T> source)
            {
                _source = source;
                _info = new PagingInfo(source.PageSize);
            }

            public async Task<bool> MoveNextAsync(CancellationToken ct = default)
            {
                if (_info.Remaining == false)
                    return false;

                var pageResult = await _source._getPage(_info, ct).ConfigureAwait(false);

                pageResult.Switch(data => Current = data)
                    .Switch(error => { });

                if (Current == null)
                    return false;

                _info.Page++;
                _info.Remaining = _source._nextPage(_info, Current);

                return true;
            }

            public void Dispose()
            {
                Current = null;
            }
        }
    }
}
