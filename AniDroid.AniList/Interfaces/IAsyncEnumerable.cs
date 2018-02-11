namespace AniDroid.AniList.Interfaces
{
    /// <summary>
    /// Asynchronous version of the <see cref="System.Collections.Generic.IEnumerable{T}"/>,
    /// allowing elements of the enumerable sequence to be retrieved asynchronously.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public interface IAsyncEnumerable<out T>
    {
        /// <summary>
        /// Gets an asynchronous enumerator over the sequence.
        /// </summary>
        /// <returns>Enumerator for asynchronous enumeration over the sequence.</returns>
        IAsyncEnumerator<T> GetEnumerator();
    }
}
