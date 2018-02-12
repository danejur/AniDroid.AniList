using System;
using System.Threading;
using System.Threading.Tasks;

namespace AniDroid.AniList.Interfaces
{
    /// <summary>
    /// Filler interface until C# 8.0
    /// Asynchronous version of the <see cref="T:System.Collections.Generic.IEnumerable`1" />, 
    /// allowing elements to be retrieved asychronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncEnumerator<out T> : IDisposable
    {
        /// <summary>
        /// Gets the current element in the iteration
        /// </summary>
        T Current { get; }

        /// <summary>
        /// Advances the enumerator to the next element in the sequence, 
        /// returning the result asyncronously.
        /// </summary>
        /// <param name="ct">Cancelllation Token that can be used to 
        /// cancel the operation</param>
        /// <returns>
        /// Task containing the result of the operation: true if the 
        /// enumerator was successfully advanced to the next element; 
        /// false if the enumerator has passed the end of the squence.
        /// </returns>
        Task<bool> MoveNextAsync(CancellationToken ct);
    }
}
