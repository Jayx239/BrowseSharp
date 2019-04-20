using BrowseSharp.History;
using System.Threading.Tasks;

namespace BrowseSharp.History
{
    /// <summary>
    /// Async browser history methods
    /// </summary>
    public interface IHistoryAsync
    {
        /// <summary>
        /// Method for navigating to last browser state by re-issuing the previous request asynchronously
        /// </summary>
        Task<IDocument> BackAsync();

        /// <summary>
        /// Method for navigating to last browser state asynchronously
        /// </summary>
        /// <param name="useCache">Determines whether to re-issue request or reload last document</param>
        Task<IDocument> BackAsync(bool useCache);

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <returns>Forward history document</returns>
        Task<IDocument> ForwardAsync();

        /// <summary>
        /// Navigate to next document in forward history asynchronously
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns>Forward history document</returns>
        Task<IDocument> ForwardAsync(bool useCache);

        /// <summary>
        /// Refresh page, re-submits last request asynchronously
        /// </summary>
        /// <returns>Current history document</returns>
        Task<IDocument> RefreshAsync();
    }
}
